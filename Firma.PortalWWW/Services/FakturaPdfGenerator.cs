using Firma.Data.Data.Sklep;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Firma.PortalWWW.Services
{
    public static class FakturaPdfGenerator
    {
        public static byte[] Generuj(Zamowienie zamowienie)
        {
            // Ustawienie licencji QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(document =>
            {
                document.Page(page =>
                {
                    // Ustawienia strony
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(text => text.FontSize(10));
                    // Nag³ówek faktury
                    page.Header().Column(header =>
                    {
                        header.Item().Text("FAKTURA").FontSize(24).Bold();
                        header.Item().Text($"Numer: FV/{zamowienie.IdZamowienie}/{zamowienie.DataZamowienia:yyyy}");
                        header.Item().Text($"Data wystawienia: {zamowienie.DataZamowienia:dd.MM.yyyy HH:mm}");
                    });
                    //odstêp 25mm od nag³ówka
                    page.Content().PaddingVertical(25).Column(content =>
                    {
                        //odstêpy liniowe 18mm
                        content.Spacing(18);

                        content.Item().Row(row =>
                        {
                            //dwie równe kolumny sprzedawca i nabywca
                            row.RelativeItem().Column(column =>
                            {
                                column.Item().Text("Sprzedawca").Bold();
                                column.Item().Text("Firma - sklep internetowy");
                                column.Item().Text("NIP: 000-000-00-00");
                                column.Item().Text("E-mail: sklep@example.com");
                            });

                            row.RelativeItem().Column(column =>
                            {
                                column.Item().Text("Nabywca").Bold();
                                column.Item().Text($"{zamowienie.Imie} {zamowienie.Nazwisko}");
                                column.Item().Text($"{zamowienie.Ulica} {zamowienie.NumerBudynku}");
                                column.Item().Text($"{zamowienie.KodPocztowy} {zamowienie.Miejscowosc}");
                                column.Item().Text(zamowienie.Email);
                            });
                        });
                        //tabela z pozycjami zamówienia
                        content.Item().Table(table =>
                        {
                            //szerokoœci kolumn
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(30);
                                columns.RelativeColumn();
                                columns.ConstantColumn(55);
                                columns.ConstantColumn(70);
                                columns.ConstantColumn(75);
                            });

                            //nag³ówki tabeli
                            table.Header(header =>
                            {
                                HeaderCell(header.Cell(), "Lp.");
                                HeaderCell(header.Cell(), "Towar");
                                HeaderCell(header.Cell(), "Ilosc");
                                HeaderCell(header.Cell(), "Cena");
                                HeaderCell(header.Cell(), "Wartosc");
                            });
                            //pozycje zamówienia
                            var lp = 1;
                            foreach (var pozycja in zamowienie.Pozycje)
                            {
                                BodyCell(table.Cell(), lp.ToString());
                                BodyCell(table.Cell(), pozycja.Towar?.Nazwa ?? $"Towar #{pozycja.TowarId}");
                                BodyCell(table.Cell(), pozycja.Ilosc.ToString());
                                BodyCell(table.Cell(), Kwota(pozycja.Cena));
                                BodyCell(table.Cell(), Kwota(pozycja.Cena * pozycja.Ilosc));
                                lp++;
                            }
                        });

                        content.Item().AlignRight().Text($"Razem do zaplaty: {Kwota(zamowienie.Suma)}").FontSize(14).Bold();

                        var platnosc = zamowienie.Platnosci.FirstOrDefault();
                        if (platnosc != null)
                        {
                            content.Item().Text($"Metoda platnosci: {platnosc.Metoda}");
                            content.Item().Text($"Status platnosci: {platnosc.Status}");
                        }
                    });

                    page.Footer().AlignCenter().Text("Dokument wygenerowany automatycznie po zlozeniu zamowienia.");
                });
            }).GeneratePdf();
        }

        private static void HeaderCell(IContainer container, string text)
        {
            container
                .Background(Colors.Grey.Lighten3)
                .Border(1)
                .Padding(5)
                .Text(text)
                .Bold();
        }

        private static void BodyCell(IContainer container, string text)
        {
            container
                .Border(1)
                .Padding(5)
                .Text(text);
        }

        private static string Kwota(decimal value)
        {
            return $"{value:0.00} zl";
        }
    }
}
