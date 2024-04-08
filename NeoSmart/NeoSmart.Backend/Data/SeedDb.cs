using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.BackEnd.Services;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Interfaces;
using NeoSmart.ClassLibraries.Responses;
using NeoSmart.Data.Entities;
using System.ComponentModel.Design;

namespace NeoSmart.BackEnd.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            //await CheckCountriesAsync();
            await CheckResourceTypesAsync();
            await CheckDocumentTypesAsync();
            await CheckCompanysAsync();
            await CheckProcessesAsync();
            await CheckOccupationsAsync();
            await CheckFormationsAsync();
            await CheckTopicsAsync();
            //await CheckTrainingCalendarAsync();
            await CheckTrainingsAsync();
            await CheckRolesAsycn();
            await CheckSlider();
            await CheckUserAsync(null, "1090388348", "Daniel", "Oicata Hernandez", "danieloicata1125413@correo.itm.edu.co", "3177457755", "CARRERA", "Duitama", UserType.SuperAdmin, "1090Jeep$");
            await CheckUserAsync(null, "43993064", "Elizabet", "Loaiza Garcia", "elizabetloaiza1125440@correo.itm.edu.co", "3104995761", "CARRERA", "Medellín", UserType.SuperAdmin, "Inicio123*");
            await CheckUserAsync(null, "15374665", "Henry Alonso", "Muñoz Carvajal", "henrymunoz1125401@correo.itm.edu.co", "3218399637", "CARRERA", "Medellín", UserType.SuperAdmin, "Inicio123*");
            await CheckUserAsync("830118667-1", "10903883481", "Daniel", "Oicata Hernandez", "danielandres011@hotmail.com", "3177457755", "CARRERA", "Duitama", UserType.Admin, "1090Jeep$");
        }
        private async Task CheckRolesAsycn()
        {
            await _userHelper.CheckRoleAsync(UserType.SuperAdmin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Manager.ToString());
            await _userHelper.CheckRoleAsync(UserType.Leader.ToString());
            await _userHelper.CheckRoleAsync(UserType.Trainer.ToString());
            await _userHelper.CheckRoleAsync(UserType.Employee.ToString());
        }
        private async Task CheckResourceTypesAsync()
        {
            if (!_context.ResourceTypes.Any())
            {
                _context.ResourceTypes.Add(new ResourceType { Name = "FILE", Status = true });
                _context.ResourceTypes.Add(new ResourceType { Name = "VIDEO", Status = true });
                _context.ResourceTypes.Add(new ResourceType { Name = "LINK", Status = true });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckDocumentTypesAsync()
        {
            if (!_context.DocumentTypes.Any())
            {
                _context.DocumentTypes.Add(new DocumentType { Name = "NIT", Description = "NUMERO DE IDENTIFICACION TRIBUTARIA", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "CC", Description = "CEDULA DE CIUDADANIA", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "LM", Description = "LIBRETA MILITAR", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "TI", Description = "TARJETA DE IDENTIDAD", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "TE", Description = "TARJETA DE EXTRANJERIA", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "TDE", Description = "TIPO DE DOCUMENTO EXTRANJERO", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "RUT", Description = "REGISTRO UNICO TRIBUTARIO", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "RC", Description = "REGISTRO CIVIL DE NACIMIENTO", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "CE", Description = "CEDULA DE EXTRANJERIA", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "PS", Description = "PASAPORTE", Status = true });
                _context.DocumentTypes.Add(new DocumentType { Name = "GENER", Description = "GENERICOS", Status = true });
                await _context.SaveChangesAsync();
            }
        }

        //private async Task CheckCountriesAsync()
        //{
        //    if (!_context.Countries.Any())
        //    {
        //        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == "COLOMBIA")!;
        //        if (country == null)
        //        {
        //            country = new() { Name = "COLOMBIA", States = new List<State>(), Status = true };
        //            var state = new State() { Name = "ANTIOQUIA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City() { Name = "ABRIAQUÍ", Status = true });
        //            state.Cities.Add(new City { Name = "ALEJANDRIA", Status = true });
        //            state.Cities.Add(new City { Name = "AMAGÁ", Status = true });
        //            state.Cities.Add(new City { Name = "AMALFI", Status = true });
        //            state.Cities.Add(new City { Name = "ANDES", Status = true });
        //            state.Cities.Add(new City { Name = "ANGELÓPOLIS", Status = true });
        //            state.Cities.Add(new City { Name = "ANGOSTURA", Status = true });
        //            state.Cities.Add(new City { Name = "ANORÍ", Status = true });
        //            state.Cities.Add(new City { Name = "ANZÁ", Status = true });
        //            state.Cities.Add(new City { Name = "APARTADÓ", Status = true });
        //            state.Cities.Add(new City { Name = "ARBOLETES", Status = true });
        //            state.Cities.Add(new City { Name = "ARGELIA", Status = true });
        //            state.Cities.Add(new City { Name = "ARMENIA", Status = true });
        //            state.Cities.Add(new City { Name = "BARBOSA", Status = true });
        //            state.Cities.Add(new City { Name = "BELLO", Status = true });
        //            state.Cities.Add(new City { Name = "BELMIRA", Status = true });
        //            state.Cities.Add(new City { Name = "BETANIA", Status = true });
        //            state.Cities.Add(new City { Name = "BETULIA", Status = true });
        //            state.Cities.Add(new City { Name = "BOLÍVAR", Status = true });
        //            state.Cities.Add(new City { Name = "BRICEÑO", Status = true });
        //            state.Cities.Add(new City { Name = "BURÍTICA", Status = true });
        //            state.Cities.Add(new City { Name = "CAICEDO", Status = true });
        //            state.Cities.Add(new City { Name = "CALDAS", Status = true });
        //            state.Cities.Add(new City { Name = "CAMPAMENTO", Status = true });
        //            state.Cities.Add(new City { Name = "CARACOLÍ", Status = true });
        //            state.Cities.Add(new City { Name = "CARAMANTA", Status = true });
        //            state.Cities.Add(new City { Cod = 165, Name = "CAREPA", Status = true });
        //            state.Cities.Add(new City { Cod = 168, Name = "CARMEN DE VIBORAL", Status = true });
        //            state.Cities.Add(new City { Cod = 170, Name = "CAROLINA", Status = true });
        //            state.Cities.Add(new City { Cod = 177, Name = "CAUCASIA", Status = true });
        //            state.Cities.Add(new City { Cod = 178, Name = "CAÑASGORDAS", Status = true });
        //            state.Cities.Add(new City { Cod = 190, Name = "CHIGORODÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 217, Name = "CISNEROS", Status = true });
        //            state.Cities.Add(new City { Cod = 222, Name = "COCORNÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 229, Name = "CONCEPCIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 231, Name = "CONCORDIA", Status = true });
        //            state.Cities.Add(new City { Cod = 239, Name = "COPACABANA", Status = true });
        //            state.Cities.Add(new City { Cod = 267, Name = "CÁCERES", Status = true });
        //            state.Cities.Add(new City { Cod = 276, Name = "DABEIBA", Status = true });
        //            state.Cities.Add(new City { Cod = 281, Name = "DON MATÍAS", Status = true });
        //            state.Cities.Add(new City { Cod = 285, Name = "EBÉJICO", Status = true });
        //            state.Cities.Add(new City { Cod = 286, Name = "EL BAGRE", Status = true });
        //            state.Cities.Add(new City { Cod = 329, Name = "ENTRERRÍOS", Status = true });
        //            state.Cities.Add(new City { Cod = 330, Name = "ENVIGADO", Status = true });
        //            state.Cities.Add(new City { Cod = 348, Name = "FREDONIA", Status = true });
        //            state.Cities.Add(new City { Cod = 350, Name = "FRONTINO", Status = true });
        //            state.Cities.Add(new City { Cod = 371, Name = "GIRALDO", Status = true });
        //            state.Cities.Add(new City { Cod = 373, Name = "GIRARDOTA", Status = true });
        //            state.Cities.Add(new City { Cod = 377, Name = "GRANADA", Status = true });
        //            state.Cities.Add(new City { Cod = 387, Name = "GUADALUPE", Status = true });
        //            state.Cities.Add(new City { Cod = 399, Name = "GUARNE", Status = true });
        //            state.Cities.Add(new City { Cod = 401, Name = "GUATAPÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 416, Name = "GÓMEZ PLATA", Status = true });
        //            state.Cities.Add(new City { Cod = 422, Name = "HELICONIA", Status = true });
        //            state.Cities.Add(new City { Cod = 425, Name = "HISPANIA", Status = true });
        //            state.Cities.Add(new City { Cod = 437, Name = "ITAGÜÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 438, Name = "ITUANGO", Status = true });
        //            state.Cities.Add(new City { Cod = 442, Name = "JARDÍN", Status = true });
        //            state.Cities.Add(new City { Cod = 444, Name = "JERICÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 457, Name = "LA CEJA", Status = true });
        //            state.Cities.Add(new City { Cod = 463, Name = "LA ESTRELLA", Status = true });
        //            state.Cities.Add(new City { Cod = 477, Name = "LA PINTADA", Status = true });
        //            state.Cities.Add(new City { Cod = 485, Name = "LA UNIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 504, Name = "LIBORINA", Status = true });
        //            state.Cities.Add(new City { Cod = 519, Name = "MACEO", Status = true });
        //            state.Cities.Add(new City { Cod = 538, Name = "MARINILLA", Status = true });
        //            state.Cities.Add(new City { Cod = 547, Name = "MEDELLÍN", Status = true });
        //            state.Cities.Add(new City { Cod = 569, Name = "MONTEBELLO", Status = true });
        //            state.Cities.Add(new City { Cod = 584, Name = "MURINDÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 585, Name = "MUTATÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 589, Name = "NARIÑO", Status = true });
        //            state.Cities.Add(new City { Cod = 593, Name = "NECHÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 594, Name = "NECOCLÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 615, Name = "OLAYA", Status = true });
        //            state.Cities.Add(new City { Cod = 656, Name = "PEQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 659, Name = "PEÑOL", Status = true });
        //            state.Cities.Add(new City { Cod = 689, Name = "PUEBLORRICO", Status = true });
        //            state.Cities.Add(new City { Cod = 694, Name = "PUERTO BERRÍO", Status = true });
        //            state.Cities.Add(new City { Cod = 707, Name = "PUERTO NARE", Status = true });
        //            state.Cities.Add(new City { Cod = 716, Name = "PUERTO TRIUNFO", Status = true });
        //            state.Cities.Add(new City { Cod = 738, Name = "REMEDIOS", Status = true });
        //            state.Cities.Add(new City { Cod = 743, Name = "RETIRO", Status = true });
        //            state.Cities.Add(new City { Cod = 764, Name = "RÍONEGRO", Status = true });
        //            state.Cities.Add(new City { Cod = 768, Name = "SABANALARGA", Status = true });
        //            state.Cities.Add(new City { Cod = 772, Name = "SABANETA", Status = true });
        //            state.Cities.Add(new City { Cod = 781, Name = "SALGAR", Status = true });
        //            state.Cities.Add(new City { Cod = 790, Name = "SAN ANDRÉS DE CUERQUÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 800, Name = "SAN CARLOS", Status = true });
        //            state.Cities.Add(new City { Cod = 810, Name = "SAN FRANCISCO", Status = true });
        //            state.Cities.Add(new City { Cod = 816, Name = "SAN JERÓNIMO", Status = true });
        //            state.Cities.Add(new City { Cod = 820, Name = "SAN JOSÉ DE MONTAÑA", Status = true });
        //            state.Cities.Add(new City { Cod = 831, Name = "SAN JUAN DE URABÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 836, Name = "SAN LUÍS", Status = true });
        //            state.Cities.Add(new City { Cod = 851, Name = "SAN PEDRO", Status = true });
        //            state.Cities.Add(new City { Cod = 855, Name = "SAN PEDRO DE URABÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 857, Name = "SAN RAFAEL", Status = true });
        //            state.Cities.Add(new City { Cod = 858, Name = "SAN ROQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 861, Name = "SAN VICENTE", Status = true });
        //            state.Cities.Add(new City { Cod = 867, Name = "SANTA BÁRBARA", Status = true });
        //            state.Cities.Add(new City { Cod = 872, Name = "SANTA FÉ DE ANTIOQUIA", Status = true });
        //            state.Cities.Add(new City { Cod = 883, Name = "SANTA ROSA DE OSOS", Status = true });
        //            state.Cities.Add(new City { Cod = 892, Name = "SANTO DOMINGO", Status = true });
        //            state.Cities.Add(new City { Cod = 894, Name = "SANTUARIO", Status = true });
        //            state.Cities.Add(new City { Cod = 902, Name = "SEGOVIA", Status = true });
        //            state.Cities.Add(new City { Cod = 928, Name = "SONSÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 929, Name = "SOPETRÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 965, Name = "TARAZÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 967, Name = "TARSO", Status = true });
        //            state.Cities.Add(new City { Cod = 991, Name = "TITIRIBÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 996, Name = "TOLEDO", Status = true });
        //            state.Cities.Add(new City { Cod = 1017, Name = "TURBO", Status = true });
        //            state.Cities.Add(new City { Cod = 1022, Name = "TÁMESIS", Status = true });
        //            state.Cities.Add(new City { Cod = 1031, Name = "URAMITA", Status = true });
        //            state.Cities.Add(new City { Cod = 1034, Name = "URRAO", Status = true });
        //            state.Cities.Add(new City { Cod = 1037, Name = "VALDIVIA", Status = true });
        //            state.Cities.Add(new City { Cod = 1043, Name = "VALPARAISO", Status = true });
        //            state.Cities.Add(new City { Cod = 1045, Name = "VEGACHÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 1047, Name = "VENECIA", Status = true });
        //            state.Cities.Add(new City { Cod = 1054, Name = "VIGÍA DEL FUERTE", Status = true });
        //            state.Cities.Add(new City { Cod = 1081, Name = "YALÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 1082, Name = "YARUMAL", Status = true });
        //            state.Cities.Add(new City { Cod = 1083, Name = "YOLOMBÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 1084, Name = "YONDÓ (CASABE)", Status = true });
        //            state.Cities.Add(new City { Cod = 1091, Name = "ZARAGOZA", Status = true });
        //            country.States.Add(state);
        //            state = new State() { Cod = 8, Name = "ATLÁNTICO", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 78, Name = "BARANOA", Status = true });
        //            state.Cities.Add(new City { Cod = 88, Name = "BARRANQUILLA", Status = true });
        //            state.Cities.Add(new City { Cod = 152, Name = "CAMPO DE LA CRUZ", Status = true });
        //            state.Cities.Add(new City { Cod = 156, Name = "CANDELARIA", Status = true });
        //            state.Cities.Add(new City { Cod = 362, Name = "GALAPA", Status = true });
        //            state.Cities.Add(new City { Cod = 449, Name = "JUAN DE ACOSTA", Status = true });
        //            state.Cities.Add(new City { Cod = 513, Name = "LURUACO", Status = true });
        //            state.Cities.Add(new City { Cod = 527, Name = "MALAMBO", Status = true });
        //            state.Cities.Add(new City { Cod = 529, Name = "MANATÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 637, Name = "PALMAR DE VARELA", Status = true });
        //            state.Cities.Add(new City { Cod = 668, Name = "PIOJO", Status = true });
        //            state.Cities.Add(new City { Cod = 677, Name = "POLONUEVO", Status = true });
        //            state.Cities.Add(new City { Cod = 678, Name = "PONEDERA", Status = true });
        //            state.Cities.Add(new City { Cod = 698, Name = "PUERTO COLOMBIA", Status = true });
        //            state.Cities.Add(new City { Cod = 740, Name = "REPELÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 767, Name = "SABANAGRANDE", Status = true });
        //            state.Cities.Add(new City { Cod = 769, Name = "SABANALARGA", Status = true });
        //            state.Cities.Add(new City { Cod = 876, Name = "SANTA LUCÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 893, Name = "SANTO TOMÁS", Status = true });
        //            state.Cities.Add(new City { Cod = 925, Name = "SOLEDAD", Status = true });
        //            state.Cities.Add(new City { Cod = 938, Name = "SUAN", Status = true });
        //            state.Cities.Add(new City { Cod = 1009, Name = "TUBARÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1036, Name = "USIACURI", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 11, Name = "BOGOTÁ, D.C.", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 107, Name = "BOGOTÁ D.C.", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 13, Name = "BOLÍVAR", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 5, Name = "ACHÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 28, Name = "ALTOS DEL ROSARIO", Status = true });
        //            state.Cities.Add(new City { Cod = 59, Name = "ARENAL", Status = true });
        //            state.Cities.Add(new City { Cod = 64, Name = "ARJONA", Status = true });
        //            state.Cities.Add(new City { Cod = 68, Name = "ARROYOHONDO", Status = true });
        //            state.Cities.Add(new City { Cod = 87, Name = "BARRANCO DE LOBA", Status = true });
        //            state.Cities.Add(new City { Cod = 141, Name = "CALAMAR", Status = true });
        //            state.Cities.Add(new City { Cod = 158, Name = "CANTAGALLO", Status = true });
        //            state.Cities.Add(new City { Cod = 171, Name = "CARTAGENA", Status = true });
        //            state.Cities.Add(new City { Cod = 214, Name = "CICUCO", Status = true });
        //            state.Cities.Add(new City { Cod = 221, Name = "CLEMENCIA", Status = true });
        //            state.Cities.Add(new City { Cod = 273, Name = "CÓRDOBA", Status = true });
        //            state.Cities.Add(new City { Cod = 293, Name = "EL CARMEN DE BOLÍVAR", Status = true });
        //            state.Cities.Add(new City { Cod = 305, Name = "EL GUAMO", Status = true });
        //            state.Cities.Add(new City { Cod = 310, Name = "EL PEÑON", Status = true });
        //            state.Cities.Add(new City { Cod = 418, Name = "HATILLO DE LOBA", Status = true });
        //            state.Cities.Add(new City { Cod = 522, Name = "MAGANGUÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 524, Name = "MAHATES", Status = true });
        //            state.Cities.Add(new City { Cod = 537, Name = "MARGARITA", Status = true });
        //            state.Cities.Add(new City { Cod = 545, Name = "MARÍA LA BAJA", Status = true });
        //            state.Cities.Add(new City { Cod = 565, Name = "MOMPÓS", Status = true });
        //            state.Cities.Add(new City { Cod = 570, Name = "MONTECRISTO", Status = true });
        //            state.Cities.Add(new City { Cod = 575, Name = "MORALES", Status = true });
        //            state.Cities.Add(new City { Cod = 603, Name = "NOROSÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 667, Name = "PINILLOS", Status = true });
        //            state.Cities.Add(new City { Cod = 737, Name = "REGIDOR", Status = true });
        //            state.Cities.Add(new City { Cod = 762, Name = "RÍO VIEJO", Status = true });
        //            state.Cities.Add(new City { Cod = 805, Name = "SAN CRISTOBAL", Status = true });
        //            state.Cities.Add(new City { Cod = 808, Name = "SAN ESTANISLAO", Status = true });
        //            state.Cities.Add(new City { Cod = 809, Name = "SAN FERNANDO", Status = true });
        //            state.Cities.Add(new City { Cod = 814, Name = "SAN JACINTO", Status = true });
        //            state.Cities.Add(new City { Cod = 815, Name = "SAN JACINTO DEL CAUCA", Status = true });
        //            state.Cities.Add(new City { Cod = 828, Name = "SAN JUAN DE NEPOMUCENO", Status = true });
        //            state.Cities.Add(new City { Cod = 842, Name = "SAN MARTÍN DE LOBA", Status = true });
        //            state.Cities.Add(new City { Cod = 848, Name = "SAN PABLO", Status = true });
        //            state.Cities.Add(new City { Cod = 871, Name = "SANTA CATALINA", Status = true });
        //            state.Cities.Add(new City { Cod = 880, Name = "SANTA ROSA", Status = true });
        //            state.Cities.Add(new City { Cod = 885, Name = "SANTA ROSA DEL SUR", Status = true });
        //            state.Cities.Add(new City { Cod = 913, Name = "SIMITÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 930, Name = "SOPLAVIENTO", Status = true });
        //            state.Cities.Add(new City { Cod = 959, Name = "TALAIGUA NUEVO", Status = true });
        //            state.Cities.Add(new City { Cod = 990, Name = "TIQUISIO (PUERTO RICO)", Status = true });
        //            state.Cities.Add(new City { Cod = 1015, Name = "TURBACO", Status = true });
        //            state.Cities.Add(new City { Cod = 1016, Name = "TURBANÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1064, Name = "VILLANUEVA", Status = true });
        //            state.Cities.Add(new City { Cod = 1088, Name = "ZAMBRANO", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 15, Name = "BOYACÁ", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 24, Name = "ALMEIDA", Status = true });
        //            state.Cities.Add(new City { Cod = 48, Name = "AQUITANIA", Status = true });
        //            state.Cities.Add(new City { Cod = 58, Name = "ARCABUCO", Status = true });
        //            state.Cities.Add(new City { Cod = 94, Name = "BELÉN", Status = true });
        //            state.Cities.Add(new City { Cod = 99, Name = "BERBEO", Status = true });
        //            state.Cities.Add(new City { Cod = 101, Name = "BETEITIVA", Status = true });
        //            state.Cities.Add(new City { Cod = 105, Name = "BOAVITA", Status = true });
        //            state.Cities.Add(new City { Cod = 115, Name = "BOYACÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 117, Name = "BRICEÑO", Status = true });
        //            state.Cities.Add(new City { Cod = 121, Name = "BUENAVISTA", Status = true });
        //            state.Cities.Add(new City { Cod = 130, Name = "BUSBANZA", Status = true });
        //            state.Cities.Add(new City { Cod = 145, Name = "CALDAS", Status = true });
        //            state.Cities.Add(new City { Cod = 154, Name = "CAMPOHERMOSO", Status = true });
        //            state.Cities.Add(new City { Cod = 181, Name = "CERINZA", Status = true });
        //            state.Cities.Add(new City { Cod = 194, Name = "CHINAVITA", Status = true });
        //            state.Cities.Add(new City { Cod = 200, Name = "CHIQUINQUIRÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 202, Name = "CHISCAS", Status = true });
        //            state.Cities.Add(new City { Cod = 203, Name = "CHITA", Status = true });
        //            state.Cities.Add(new City { Cod = 205, Name = "CHITARAQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 206, Name = "CHIVATÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 212, Name = "CHÍQUIZA", Status = true });
        //            state.Cities.Add(new City { Cod = 213, Name = "CHÍVOR", Status = true });
        //            state.Cities.Add(new City { Cod = 218, Name = "CIÉNAGA", Status = true });
        //            state.Cities.Add(new City { Cod = 240, Name = "COPER", Status = true });
        //            state.Cities.Add(new City { Cod = 245, Name = "CORRALES", Status = true });
        //            state.Cities.Add(new City { Cod = 248, Name = "COVARACHÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 254, Name = "CUBARÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 255, Name = "CUCAITA", Status = true });
        //            state.Cities.Add(new City { Cod = 258, Name = "CUITIVA", Status = true });
        //            state.Cities.Add(new City { Cod = 272, Name = "CÓMBITA", Status = true });
        //            state.Cities.Add(new City { Cod = 283, Name = "DUITAMA", Status = true });
        //            state.Cities.Add(new City { Cod = 297, Name = "EL COCUY", Status = true });
        //            state.Cities.Add(new City { Cod = 303, Name = "EL ESPINO", Status = true });
        //            state.Cities.Add(new City { Cod = 336, Name = "FIRAVITOBA", Status = true });
        //            state.Cities.Add(new City { Cod = 340, Name = "FLORESTA", Status = true });
        //            state.Cities.Add(new City { Cod = 360, Name = "GACHANTIVÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 367, Name = "GARAGOA", Status = true });
        //            state.Cities.Add(new City { Cod = 381, Name = "GUACAMAYAS", Status = true });
        //            state.Cities.Add(new City { Cod = 404, Name = "GUATEQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 408, Name = "GUAYATÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 410, Name = "GUICÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 414, Name = "GÁMEZA", Status = true });
        //            state.Cities.Add(new City { Cod = 439, Name = "IZÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 443, Name = "JENESANO", Status = true });
        //            state.Cities.Add(new City { Cod = 445, Name = "JERICÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 456, Name = "LA CAPILLA", Status = true });
        //            state.Cities.Add(new City { Cod = 489, Name = "LA UVITA", Status = true });
        //            state.Cities.Add(new City { Cod = 492, Name = "LA VICTORIA", Status = true });
        //            state.Cities.Add(new City { Cod = 497, Name = "LABRANZAGRANDE", Status = true });
        //            state.Cities.Add(new City { Cod = 517, Name = "MACANAL", Status = true });
        //            state.Cities.Add(new City { Cod = 539, Name = "MARIPÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 556, Name = "MIRAFLORES", Status = true });
        //            state.Cities.Add(new City { Cod = 566, Name = "MONGUA", Status = true });
        //            state.Cities.Add(new City { Cod = 567, Name = "MONGUÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 568, Name = "MONIQUIRÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 581, Name = "MOTAVITA", Status = true });
        //            state.Cities.Add(new City { Cod = 587, Name = "MUZO", Status = true });
        //            state.Cities.Add(new City { Cod = 600, Name = "NOBSA", Status = true });
        //            state.Cities.Add(new City { Cod = 606, Name = "NUEVO COLÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 614, Name = "OICATÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 623, Name = "OTANCHE", Status = true });
        //            state.Cities.Add(new City { Cod = 625, Name = "PACHAVITA", Status = true });
        //            state.Cities.Add(new City { Cod = 631, Name = "PAIPA", Status = true });
        //            state.Cities.Add(new City { Cod = 632, Name = "PAJARITO", Status = true });
        //            state.Cities.Add(new City { Cod = 645, Name = "PANQUEBA", Status = true });
        //            state.Cities.Add(new City { Cod = 649, Name = "PAUNA", Status = true });
        //            state.Cities.Add(new City { Cod = 650, Name = "PAYA", Status = true });
        //            state.Cities.Add(new City { Cod = 652, Name = "PAZ DE RÍO", Status = true });
        //            state.Cities.Add(new City { Cod = 658, Name = "PESCA", Status = true });
        //            state.Cities.Add(new City { Cod = 669, Name = "PISVA", Status = true });
        //            state.Cities.Add(new City { Cod = 695, Name = "PUERTO BOYACÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 724, Name = "PÁEZ", Status = true });
        //            state.Cities.Add(new City { Cod = 732, Name = "QUIPAMA", Status = true });
        //            state.Cities.Add(new City { Cod = 735, Name = "RAMIRIQUÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 755, Name = "RONDÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 758, Name = "RÁQUIRA", Status = true });
        //            state.Cities.Add(new City { Cod = 773, Name = "SABOYÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 782, Name = "SAMACÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 807, Name = "SAN EDUARDO", Status = true });
        //            state.Cities.Add(new City { Cod = 821, Name = "SAN JOSÉ DE PARE", Status = true });
        //            state.Cities.Add(new City { Cod = 837, Name = "SAN LUÍS DE GACENO", Status = true });
        //            state.Cities.Add(new City { Cod = 843, Name = "SAN MATEO", Status = true });
        //            state.Cities.Add(new City { Cod = 846, Name = "SAN MIGUEL DE SEMA", Status = true });
        //            state.Cities.Add(new City { Cod = 850, Name = "SAN PABLO DE BORBUR", Status = true });
        //            state.Cities.Add(new City { Cod = 878, Name = "SANTA MARÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 884, Name = "SANTA ROSA DE VITERBO", Status = true });
        //            state.Cities.Add(new City { Cod = 887, Name = "SANTA SOFÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 888, Name = "SANTANA", Status = true });
        //            state.Cities.Add(new City { Cod = 900, Name = "SATIVANORTE", Status = true });
        //            state.Cities.Add(new City { Cod = 901, Name = "SATIVASUR", Status = true });
        //            state.Cities.Add(new City { Cod = 905, Name = "SIACHOQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 919, Name = "SOATÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 920, Name = "SOCHA", Status = true });
        //            state.Cities.Add(new City { Cod = 922, Name = "SOCOTÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 923, Name = "SOGAMOSO", Status = true });
        //            state.Cities.Add(new City { Cod = 927, Name = "SOMONDOCO", Status = true });
        //            state.Cities.Add(new City { Cod = 932, Name = "SORA", Status = true });
        //            state.Cities.Add(new City { Cod = 933, Name = "SORACÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 934, Name = "SOTAQUIRÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 949, Name = "SUSACÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 950, Name = "SUTAMARCHÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 952, Name = "SUTATENZA", Status = true });
        //            state.Cities.Add(new City { Cod = 956, Name = "SÁCHICA", Status = true });
        //            state.Cities.Add(new City { Cod = 968, Name = "TASCO", Status = true });
        //            state.Cities.Add(new City { Cod = 975, Name = "TENZA", Status = true });
        //            state.Cities.Add(new City { Cod = 980, Name = "TIBANÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 981, Name = "TIBASOSA", Status = true });
        //            state.Cities.Add(new City { Cod = 988, Name = "TINJACÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 989, Name = "TIPACOQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 992, Name = "TOCA", Status = true });
        //            state.Cities.Add(new City { Cod = 995, Name = "TOGUÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 1001, Name = "TOPAGÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1005, Name = "TOTA", Status = true });
        //            state.Cities.Add(new City { Cod = 1013, Name = "TUNJA", Status = true });
        //            state.Cities.Add(new City { Cod = 1014, Name = "TUNUNGUA", Status = true });
        //            state.Cities.Add(new City { Cod = 1018, Name = "TURMEQUÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 1019, Name = "TUTA", Status = true });
        //            state.Cities.Add(new City { Cod = 1020, Name = "TUTASÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1049, Name = "VENTAQUEMADA", Status = true });
        //            state.Cities.Add(new City { Cod = 1058, Name = "VILLA DE LEIVA", Status = true });
        //            state.Cities.Add(new City { Cod = 1074, Name = "VIRACACHÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1093, Name = "ZETAQUIRÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1099, Name = "ÚMBITA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 17, Name = "CALDAS", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 10, Name = "AGUADAS", Status = true });
        //            state.Cities.Add(new City { Cod = 41, Name = "ANSERMA", Status = true });
        //            state.Cities.Add(new City { Cod = 50, Name = "ARANZAZU", Status = true });
        //            state.Cities.Add(new City { Cod = 90, Name = "BELALCÁZAR", Status = true });
        //            state.Cities.Add(new City { Cod = 195, Name = "CHINCHINÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 334, Name = "FILADELFIA", Status = true });
        //            state.Cities.Add(new City { Cod = 461, Name = "LA DORADA", Status = true });
        //            state.Cities.Add(new City { Cod = 470, Name = "LA MERCED", Status = true });
        //            state.Cities.Add(new City { Cod = 493, Name = "LA VICTORIA", Status = true });
        //            state.Cities.Add(new City { Cod = 532, Name = "MANIZALES", Status = true });
        //            state.Cities.Add(new City { Cod = 534, Name = "MANZANARES", Status = true });
        //            state.Cities.Add(new City { Cod = 541, Name = "MARMATO", Status = true });
        //            state.Cities.Add(new City { Cod = 542, Name = "MARQUETALIA", Status = true });
        //            state.Cities.Add(new City { Cod = 544, Name = "MARULANDA", Status = true });
        //            state.Cities.Add(new City { Cod = 595, Name = "NEIRA", Status = true });
        //            state.Cities.Add(new City { Cod = 602, Name = "NORCASIA", Status = true });
        //            state.Cities.Add(new City { Cod = 634, Name = "PALESTINA", Status = true });
        //            state.Cities.Add(new City { Cod = 655, Name = "PENSILVANIA", Status = true });
        //            state.Cities.Add(new City { Cod = 723, Name = "PÁCORA", Status = true });
        //            state.Cities.Add(new City { Cod = 750, Name = "RISARALDA", Status = true });
        //            state.Cities.Add(new City { Cod = 761, Name = "RÍO SUCIO", Status = true });
        //            state.Cities.Add(new City { Cod = 776, Name = "SALAMINA", Status = true });
        //            state.Cities.Add(new City { Cod = 784, Name = "SAMANÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 818, Name = "SAN JOSÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 946, Name = "SUPÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 1063, Name = "VILLAMARÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 1076, Name = "VITERBO", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 18, Name = "CAQUETÁ", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 14, Name = "ALBANIA", Status = true });
        //            state.Cities.Add(new City { Cod = 98, Name = "BELÉN DE LOS ANDAQUÍES", Status = true });
        //            state.Cities.Add(new City { Cod = 172, Name = "CARTAGENA DEL CHAIRÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 264, Name = "CURILLO", Status = true });
        //            state.Cities.Add(new City { Cod = 300, Name = "EL DONCELLO", Status = true });
        //            state.Cities.Add(new City { Cod = 308, Name = "EL PAUJIL", Status = true });
        //            state.Cities.Add(new City { Cod = 338, Name = "FLORENCIA", Status = true });
        //            state.Cities.Add(new City { Cod = 472, Name = "LA MONTAÑITA", Status = true });
        //            state.Cities.Add(new City { Cod = 555, Name = "MILÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 577, Name = "MORELIA", Status = true });
        //            state.Cities.Add(new City { Cod = 710, Name = "PUERTO RICO", Status = true });
        //            state.Cities.Add(new City { Cod = 823, Name = "SAN JOSÉ DEL FRAGUA", Status = true });
        //            state.Cities.Add(new City { Cod = 862, Name = "SAN VICENTE DEL CAGUÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 924, Name = "SOLANO", Status = true });
        //            state.Cities.Add(new City { Cod = 926, Name = "SOLITA", Status = true });
        //            state.Cities.Add(new City { Cod = 1044, Name = "VALPARAISO", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 19, Name = "CAUCA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 23, Name = "ALMAGUER", Status = true });
        //            state.Cities.Add(new City { Cod = 61, Name = "ARGELIA", Status = true });
        //            state.Cities.Add(new City { Cod = 76, Name = "BALBOA", Status = true });
        //            state.Cities.Add(new City { Cod = 111, Name = "BOLÍVAR", Status = true });
        //            state.Cities.Add(new City { Cod = 125, Name = "BUENOS AIRES", Status = true });
        //            state.Cities.Add(new City { Cod = 139, Name = "CAJIBÍO", Status = true });
        //            state.Cities.Add(new City { Cod = 146, Name = "CALDONO", Status = true });
        //            state.Cities.Add(new City { Cod = 149, Name = "CALOTO", Status = true });
        //            state.Cities.Add(new City { Cod = 242, Name = "CORINTO", Status = true });
        //            state.Cities.Add(new City { Cod = 321, Name = "EL TAMBO", Status = true });
        //            state.Cities.Add(new City { Cod = 339, Name = "FLORENCIA", Status = true });
        //            state.Cities.Add(new City { Cod = 384, Name = "GUACHENÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 397, Name = "GUAPÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 432, Name = "INZÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 440, Name = "JAMBALÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 482, Name = "LA SIERRA", Status = true });
        //            state.Cities.Add(new City { Cod = 490, Name = "LA VEGA", Status = true });
        //            state.Cities.Add(new City { Cod = 516, Name = "LÓPEZ (MICAY)", Status = true });
        //            state.Cities.Add(new City { Cod = 553, Name = "MERCADERES", Status = true });
        //            state.Cities.Add(new City { Cod = 558, Name = "MIRANDA", Status = true });
        //            state.Cities.Add(new City { Cod = 576, Name = "MORALES", Status = true });
        //            state.Cities.Add(new City { Cod = 627, Name = "PADILLA", Status = true });
        //            state.Cities.Add(new City { Cod = 648, Name = "PATÍA (EL BORDO)", Status = true });
        //            state.Cities.Add(new City { Cod = 660, Name = "PIAMONTE", Status = true });
        //            state.Cities.Add(new City { Cod = 663, Name = "PIENDAMÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 679, Name = "POPAYÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 715, Name = "PUERTO TEJADA", Status = true });
        //            state.Cities.Add(new City { Cod = 720, Name = "PURACÉ (COCONUCO)", Status = true });
        //            state.Cities.Add(new City { Cod = 725, Name = "PÁEZ (BELALCAZAR)", Status = true });
        //            state.Cities.Add(new City { Cod = 756, Name = "ROSAS", Status = true });
        //            state.Cities.Add(new City { Cod = 859, Name = "SAN SEBASTIÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 881, Name = "SANTA ROSA", Status = true });
        //            state.Cities.Add(new City { Cod = 889, Name = "SANTANDER DE QUILICHAO", Status = true });
        //            state.Cities.Add(new City { Cod = 910, Name = "SILVIA", Status = true });
        //            state.Cities.Add(new City { Cod = 935, Name = "SOTARA (PAISPAMBA)", Status = true });
        //            state.Cities.Add(new City { Cod = 941, Name = "SUCRE", Status = true });
        //            state.Cities.Add(new City { Cod = 953, Name = "SUÁREZ", Status = true });
        //            state.Cities.Add(new City { Cod = 986, Name = "TIMBIQUÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 987, Name = "TIMBÍO", Status = true });
        //            state.Cities.Add(new City { Cod = 1003, Name = "TORIBÍO", Status = true });
        //            state.Cities.Add(new City { Cod = 1006, Name = "TOTORÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 1057, Name = "VILLA RICA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 20, Name = "CESAR", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 8, Name = "AGUACHICA", Status = true });
        //            state.Cities.Add(new City { Cod = 12, Name = "AGUSTÍN CODAZZI", Status = true });
        //            state.Cities.Add(new City { Cod = 69, Name = "ASTREA", Status = true });
        //            state.Cities.Add(new City { Cod = 89, Name = "BECERRÍL", Status = true });
        //            state.Cities.Add(new City { Cod = 114, Name = "BOSCONIA", Status = true });
        //            state.Cities.Add(new City { Cod = 192, Name = "CHIMICHAGUA", Status = true });
        //            state.Cities.Add(new City { Cod = 201, Name = "CHIRIGUANÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 266, Name = "CURUMANÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 299, Name = "EL COPEY", Status = true });
        //            state.Cities.Add(new City { Cod = 307, Name = "EL PASO", Status = true });
        //            state.Cities.Add(new City { Cod = 366, Name = "GAMARRA", Status = true });
        //            state.Cities.Add(new City { Cod = 375, Name = "GONZALEZ", Status = true });
        //            state.Cities.Add(new City { Cod = 465, Name = "LA GLORIA", Status = true });
        //            state.Cities.Add(new City { Cod = 466, Name = "LA JAGUA DE IBIRICO", Status = true });
        //            state.Cities.Add(new City { Cod = 475, Name = "LA PAZ (ROBLES)", Status = true });
        //            state.Cities.Add(new City { Cod = 531, Name = "MANAURE BALCÓN DEL CESAR", Status = true });
        //            state.Cities.Add(new City { Cod = 629, Name = "PAILITAS", Status = true });
        //            state.Cities.Add(new City { Cod = 654, Name = "PELAYA", Status = true });
        //            state.Cities.Add(new City { Cod = 686, Name = "PUEBLO BELLO", Status = true });
        //            state.Cities.Add(new City { Cod = 763, Name = "RÍO DE ORO", Status = true });
        //            state.Cities.Add(new City { Cod = 787, Name = "SAN ALBERTO", Status = true });
        //            state.Cities.Add(new City { Cod = 806, Name = "SAN DIEGO", Status = true });
        //            state.Cities.Add(new City { Cod = 840, Name = "SAN MARTÍN", Status = true });
        //            state.Cities.Add(new City { Cod = 960, Name = "TAMALAMEQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 1042, Name = "VALLEDUPAR", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 23, Name = "CÓRDOBA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 72, Name = "AYAPEL", Status = true });
        //            state.Cities.Add(new City { Cod = 122, Name = "BUENAVISTA", Status = true });
        //            state.Cities.Add(new City { Cod = 155, Name = "CANALETE", Status = true });
        //            state.Cities.Add(new City { Cod = 180, Name = "CERETÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 193, Name = "CHIMÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 197, Name = "CHINÚ", Status = true });
        //            state.Cities.Add(new City { Cod = 220, Name = "CIÉNAGA DE ORO", Status = true });
        //            state.Cities.Add(new City { Cod = 247, Name = "COTORRA", Status = true });
        //            state.Cities.Add(new City { Cod = 452, Name = "LA APARTADA Y LA FRONTERA", Status = true });
        //            state.Cities.Add(new City { Cod = 507, Name = "LORICA", Status = true });
        //            state.Cities.Add(new City { Cod = 508, Name = "LOS CÓRDOBAS", Status = true });
        //            state.Cities.Add(new City { Cod = 564, Name = "MOMIL", Status = true });
        //            state.Cities.Add(new City { Cod = 571, Name = "MONTELÍBANO", Status = true });
        //            state.Cities.Add(new City { Cod = 573, Name = "MONTERIA", Status = true });
        //            state.Cities.Add(new City { Cod = 582, Name = "MOÑITOS", Status = true });
        //            state.Cities.Add(new City { Cod = 674, Name = "PLANETA RICA", Status = true });
        //            state.Cities.Add(new City { Cod = 687, Name = "PUEBLO NUEVO", Status = true });
        //            state.Cities.Add(new City { Cod = 700, Name = "PUERTO ESCONDIDO", Status = true });
        //            state.Cities.Add(new City { Cod = 704, Name = "PUERTO LIBERTADOR", Status = true });
        //            state.Cities.Add(new City { Cod = 722, Name = "PURÍSIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 774, Name = "SAHAGÚN", Status = true });
        //            state.Cities.Add(new City { Cod = 789, Name = "SAN ANDRÉS SOTAVENTO", Status = true });
        //            state.Cities.Add(new City { Cod = 791, Name = "SAN ANTERO", Status = true });
        //            state.Cities.Add(new City { Cod = 798, Name = "SAN BERNARDO DEL VIENTO", Status = true });
        //            state.Cities.Add(new City { Cod = 801, Name = "SAN CARLOS", Status = true });
        //            state.Cities.Add(new City { Cod = 822, Name = "SAN JOSÉ DE URÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 856, Name = "SAN PELAYO", Status = true });
        //            state.Cities.Add(new City { Cod = 984, Name = "TIERRALTA", Status = true });
        //            state.Cities.Add(new City { Cod = 1010, Name = "TUCHÍN", Status = true });
        //            state.Cities.Add(new City { Cod = 1038, Name = "VALENCIA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 25, Name = "CUNDINAMARCA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 7, Name = "AGUA DE DIOS", Status = true });
        //            state.Cities.Add(new City { Cod = 17, Name = "ALBÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 33, Name = "ANAPOIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 39, Name = "ANOLAIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 46, Name = "APULO", Status = true });
        //            state.Cities.Add(new City { Cod = 54, Name = "ARBELÁEZ", Status = true });
        //            state.Cities.Add(new City { Cod = 93, Name = "BELTRÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 104, Name = "BITUIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 108, Name = "BOJACÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 131, Name = "CABRERA", Status = true });
        //            state.Cities.Add(new City { Cod = 134, Name = "CACHIPAY", Status = true });
        //            state.Cities.Add(new City { Cod = 140, Name = "CAJICÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 160, Name = "CAPARRAPÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 167, Name = "CARMEN DE CARUPA", Status = true });
        //            state.Cities.Add(new City { Cod = 185, Name = "CHAGUANÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 198, Name = "CHIPAQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 208, Name = "CHOACHÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 209, Name = "CHOCONTÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 211, Name = "CHÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 224, Name = "COGUA", Status = true });
        //            state.Cities.Add(new City { Cod = 246, Name = "COTA", Status = true });
        //            state.Cities.Add(new City { Cod = 256, Name = "CUCUNUBÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 270, Name = "CÁQUEZA", Status = true });
        //            state.Cities.Add(new City { Cod = 298, Name = "EL COLEGIO", Status = true });
        //            state.Cities.Add(new City { Cod = 312, Name = "EL PEÑÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 318, Name = "EL ROSAL", Status = true });
        //            state.Cities.Add(new City { Cod = 332, Name = "FACATATIVÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 346, Name = "FOSCA", Status = true });
        //            state.Cities.Add(new City { Cod = 354, Name = "FUNZA", Status = true });
        //            state.Cities.Add(new City { Cod = 355, Name = "FUSAGASUGÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 356, Name = "FÓMEQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 357, Name = "FÚQUENE", Status = true });
        //            state.Cities.Add(new City { Cod = 358, Name = "GACHALÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 359, Name = "GACHANCIPÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 361, Name = "GACHETÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 365, Name = "GAMA", Status = true });
        //            state.Cities.Add(new City { Cod = 372, Name = "GIRARDOT", Status = true });
        //            state.Cities.Add(new City { Cod = 378, Name = "GRANADA", Status = true });
        //            state.Cities.Add(new City { Cod = 385, Name = "GUACHETÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 390, Name = "GUADUAS", Status = true });
        //            state.Cities.Add(new City { Cod = 400, Name = "GUASCA", Status = true });
        //            state.Cities.Add(new City { Cod = 402, Name = "GUATAQUÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 403, Name = "GUATAVITA", Status = true });
        //            state.Cities.Add(new City { Cod = 406, Name = "GUAYABAL DE SIQUIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 407, Name = "GUAYABETAL", Status = true });
        //            state.Cities.Add(new City { Cod = 411, Name = "GUTIÉRREZ", Status = true });
        //            state.Cities.Add(new City { Cod = 446, Name = "JERUSALÉN", Status = true });
        //            state.Cities.Add(new City { Cod = 450, Name = "JUNÍN", Status = true });
        //            state.Cities.Add(new City { Cod = 455, Name = "LA CALERA", Status = true });
        //            state.Cities.Add(new City { Cod = 471, Name = "LA MESA", Status = true });
        //            state.Cities.Add(new City { Cod = 473, Name = "LA PALMA", Status = true });
        //            state.Cities.Add(new City { Cod = 476, Name = "LA PEÑA", Status = true });
        //            state.Cities.Add(new City { Cod = 491, Name = "LA VEGA", Status = true });
        //            state.Cities.Add(new City { Cod = 502, Name = "LENGUAZAQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 520, Name = "MACHETÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 521, Name = "MADRID", Status = true });
        //            state.Cities.Add(new City { Cod = 533, Name = "MANTA", Status = true });
        //            state.Cities.Add(new City { Cod = 548, Name = "MEDINA", Status = true });
        //            state.Cities.Add(new City { Cod = 579, Name = "MOSQUERA", Status = true });
        //            state.Cities.Add(new City { Cod = 590, Name = "NARIÑO", Status = true });
        //            state.Cities.Add(new City { Cod = 597, Name = "NEMOCÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 598, Name = "NILO", Status = true });
        //            state.Cities.Add(new City { Cod = 599, Name = "NIMAIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 601, Name = "NOCAIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 626, Name = "PACHO", Status = true });
        //            state.Cities.Add(new City { Cod = 630, Name = "PAIME", Status = true });
        //            state.Cities.Add(new City { Cod = 644, Name = "PANDI", Status = true });
        //            state.Cities.Add(new City { Cod = 646, Name = "PARATEBUENO", Status = true });
        //            state.Cities.Add(new City { Cod = 647, Name = "PASCA", Status = true });
        //            state.Cities.Add(new City { Cod = 713, Name = "PUERTO SALGAR", Status = true });
        //            state.Cities.Add(new City { Cod = 718, Name = "PULÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 727, Name = "QUEBRACodGRA", Status = true });
        //            state.Cities.Add(new City { Cod = 728, Name = "QUETAME", Status = true });
        //            state.Cities.Add(new City { Cod = 733, Name = "QUIPILE", Status = true });
        //            state.Cities.Add(new City { Cod = 744, Name = "RICAURTE", Status = true });
        //            state.Cities.Add(new City { Cod = 793, Name = "SAN ANTONIO DE TEQUENDAMA", Status = true });
        //            state.Cities.Add(new City { Cod = 796, Name = "SAN BERNARDO", Status = true });
        //            state.Cities.Add(new City { Cod = 803, Name = "SAN CAYETANO", Status = true });
        //            state.Cities.Add(new City { Cod = 811, Name = "SAN FRANCISCO", Status = true });
        //            state.Cities.Add(new City { Cod = 830, Name = "SAN JUAN DE RÍO SECO", Status = true });
        //            state.Cities.Add(new City { Cod = 899, Name = "SASAIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 903, Name = "SESQUILÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 906, Name = "SIBATÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 909, Name = "SILVANIA", Status = true });
        //            state.Cities.Add(new City { Cod = 912, Name = "SIMIJACA", Status = true });
        //            state.Cities.Add(new City { Cod = 918, Name = "SOACHA", Status = true });
        //            state.Cities.Add(new City { Cod = 931, Name = "SOPÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 940, Name = "SUBACHOQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 944, Name = "SUESCA", Status = true });
        //            state.Cities.Add(new City { Cod = 945, Name = "SUPATÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 948, Name = "SUSA", Status = true });
        //            state.Cities.Add(new City { Cod = 951, Name = "SUTATAUSA", Status = true });
        //            state.Cities.Add(new City { Cod = 957, Name = "TABIO", Status = true });
        //            state.Cities.Add(new City { Cod = 970, Name = "TAUSA", Status = true });
        //            state.Cities.Add(new City { Cod = 972, Name = "TENA", Status = true });
        //            state.Cities.Add(new City { Cod = 974, Name = "TENJO", Status = true });
        //            state.Cities.Add(new City { Cod = 979, Name = "TIBACUY", Status = true });
        //            state.Cities.Add(new City { Cod = 982, Name = "TIBIRITA", Status = true });
        //            state.Cities.Add(new City { Cod = 993, Name = "TOCAIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 994, Name = "TOCANCIPÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1002, Name = "TOPAIPÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 1024, Name = "UBALÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1025, Name = "UBAQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 1026, Name = "UBATÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 1028, Name = "UNE", Status = true });
        //            state.Cities.Add(new City { Cod = 1048, Name = "VENECIA (OSPINA PÉREZ)", Status = true });
        //            state.Cities.Add(new City { Cod = 1050, Name = "VERGARA", Status = true });
        //            state.Cities.Add(new City { Cod = 1053, Name = "VIANI", Status = true });
        //            state.Cities.Add(new City { Cod = 1061, Name = "VILLAGÓMEZ", Status = true });
        //            state.Cities.Add(new City { Cod = 1068, Name = "VILLAPINZÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 1072, Name = "VILLETA", Status = true });
        //            state.Cities.Add(new City { Cod = 1073, Name = "VIOTÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1078, Name = "YACOPÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 1094, Name = "ZIPACÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 1095, Name = "ZIPAQUIRÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1100, Name = "ÚTICA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 27, Name = "CHOCÓ", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 3, Name = "ACANDÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 27, Name = "ALTO BAUDÓ (PIE DE PATO)", Status = true });
        //            state.Cities.Add(new City { Cod = 71, Name = "ATRATO (YUTO)", Status = true });
        //            state.Cities.Add(new City { Cod = 73, Name = "BAGADÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 74, Name = "BAHÍA SOLANO (MÚTIS)", Status = true });
        //            state.Cities.Add(new City { Cod = 75, Name = "BAJO BAUDÓ (PIZARRO)", Status = true });
        //            state.Cities.Add(new City { Cod = 96, Name = "BELÉN DE BAJIRÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 109, Name = "BOJAYÁ (BELLAVISTA)", Status = true });
        //            state.Cities.Add(new City { Cod = 159, Name = "CANTÓN DE SAN PABLO", Status = true });
        //            state.Cities.Add(new City { Cod = 169, Name = "CARMEN DEL DARIÉN (CURBARADÓ)", Status = true });
        //            state.Cities.Add(new City { Cod = 233, Name = "CONDOTO", Status = true });
        //            state.Cities.Add(new City { Cod = 271, Name = "CÉRTEGUI", Status = true });
        //            state.Cities.Add(new City { Cod = 292, Name = "EL CARMEN DE ATRATO", Status = true });
        //            state.Cities.Add(new City { Cod = 436, Name = "ISTMINA", Status = true });
        //            state.Cities.Add(new City { Cod = 451, Name = "JURADÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 506, Name = "LLORÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 549, Name = "MEDIO ATRATO", Status = true });
        //            state.Cities.Add(new City { Cod = 550, Name = "MEDIO BAUDÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 551, Name = "MEDIO SAN JUAN (ANDAGOYA)", Status = true });
        //            state.Cities.Add(new City { Cod = 604, Name = "NOVITA", Status = true });
        //            state.Cities.Add(new City { Cod = 608, Name = "NUQUÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 729, Name = "QUIBDÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 759, Name = "RÍO IRÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 760, Name = "RÍO QUITO", Status = true });
        //            state.Cities.Add(new City { Cod = 765, Name = "RÍOSUCIO", Status = true });
        //            state.Cities.Add(new City { Cod = 825, Name = "SAN JOSÉ DEL PALMAR", Status = true });
        //            state.Cities.Add(new City { Cod = 873, Name = "SANTA GENOVEVA DE DOCORODÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 916, Name = "SIPÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 958, Name = "TADÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 1029, Name = "UNGUÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 1030, Name = "UNIÓN PANAMERICANA (ÁNIMAS)", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 41, Name = "HUILA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 4, Name = "ACEVEDO", Status = true });
        //            state.Cities.Add(new City { Cod = 6, Name = "AGRADO", Status = true });
        //            state.Cities.Add(new City { Cod = 13, Name = "AIPE", Status = true });
        //            state.Cities.Add(new City { Cod = 22, Name = "ALGECIRAS", Status = true });
        //            state.Cities.Add(new City { Cod = 26, Name = "ALTAMIRA", Status = true });
        //            state.Cities.Add(new City { Cod = 79, Name = "BARAYA", Status = true });
        //            state.Cities.Add(new City { Cod = 153, Name = "CAMPOALEGRE", Status = true });
        //            state.Cities.Add(new City { Cod = 225, Name = "COLOMBIA", Status = true });
        //            state.Cities.Add(new City { Cod = 326, Name = "ELÍAS", Status = true });
        //            state.Cities.Add(new City { Cod = 368, Name = "GARZÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 369, Name = "GIGANTE", Status = true });
        //            state.Cities.Add(new City { Cod = 388, Name = "GUADALUPE", Status = true });
        //            state.Cities.Add(new City { Cod = 426, Name = "HOBO", Status = true });
        //            state.Cities.Add(new City { Cod = 435, Name = "ISNOS", Status = true });
        //            state.Cities.Add(new City { Cod = 453, Name = "LA ARGENTINA", Status = true });
        //            state.Cities.Add(new City { Cod = 478, Name = "LA PLATA", Status = true });
        //            state.Cities.Add(new City { Cod = 596, Name = "NEIVA", Status = true });
        //            state.Cities.Add(new City { Cod = 609, Name = "NÁTAGA", Status = true });
        //            state.Cities.Add(new City { Cod = 618, Name = "OPORAPA", Status = true });
        //            state.Cities.Add(new City { Cod = 628, Name = "PAICOL", Status = true });
        //            state.Cities.Add(new City { Cod = 633, Name = "PALERMO", Status = true });
        //            state.Cities.Add(new City { Cod = 635, Name = "PALESTINA", Status = true });
        //            state.Cities.Add(new City { Cod = 670, Name = "PITAL", Status = true });
        //            state.Cities.Add(new City { Cod = 671, Name = "PITALITO", Status = true });
        //            state.Cities.Add(new City { Cod = 751, Name = "RIVERA", Status = true });
        //            state.Cities.Add(new City { Cod = 775, Name = "SALADOBLANCO", Status = true });
        //            state.Cities.Add(new City { Cod = 786, Name = "SAN AGUSTÍN", Status = true });
        //            state.Cities.Add(new City { Cod = 879, Name = "SANTA MARÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 939, Name = "SUAZA", Status = true });
        //            state.Cities.Add(new City { Cod = 966, Name = "TARQUI", Status = true });
        //            state.Cities.Add(new City { Cod = 971, Name = "TELLO", Status = true });
        //            state.Cities.Add(new City { Cod = 977, Name = "TERUEL", Status = true });
        //            state.Cities.Add(new City { Cod = 978, Name = "TESALIA", Status = true });
        //            state.Cities.Add(new City { Cod = 985, Name = "TIMANÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1071, Name = "VILLAVIEJA", Status = true });
        //            state.Cities.Add(new City { Cod = 1080, Name = "YAGUARÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1098, Name = "ÍQUIRA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 44, Name = "LA GUAJIRA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 15, Name = "ALBANIA", Status = true });
        //            state.Cities.Add(new City { Cod = 86, Name = "BARRANCAS", Status = true });
        //            state.Cities.Add(new City { Cod = 278, Name = "DIBULLA", Status = true });
        //            state.Cities.Add(new City { Cod = 279, Name = "DISTRACCIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 306, Name = "EL MOLINO", Status = true });
        //            state.Cities.Add(new City { Cod = 344, Name = "FONSECA", Status = true });
        //            state.Cities.Add(new City { Cod = 421, Name = "HATONUEVO", Status = true });
        //            state.Cities.Add(new City { Cod = 467, Name = "LA JAGUA DEL PILAR", Status = true });
        //            state.Cities.Add(new City { Cod = 525, Name = "MAICAO", Status = true });
        //            state.Cities.Add(new City { Cod = 530, Name = "MANAURE", Status = true });
        //            state.Cities.Add(new City { Cod = 749, Name = "RIOHACHA", Status = true });
        //            state.Cities.Add(new City { Cod = 832, Name = "SAN JUAN DEL CESAR", Status = true });
        //            state.Cities.Add(new City { Cod = 1033, Name = "URIBIA", Status = true });
        //            state.Cities.Add(new City { Cod = 1035, Name = "URUMITA", Status = true });
        //            state.Cities.Add(new City { Cod = 1065, Name = "VILLANUEVA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 47, Name = "MAGDALENA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 21, Name = "ALGARROBO", Status = true });
        //            state.Cities.Add(new City { Cod = 49, Name = "ARACATACA", Status = true });
        //            state.Cities.Add(new City { Cod = 63, Name = "ARIGUANÍ (EL DIFÍCIL)", Status = true });
        //            state.Cities.Add(new City { Cod = 183, Name = "CERRO SAN ANTONIO", Status = true });
        //            state.Cities.Add(new City { Cod = 207, Name = "CHIVOLO", Status = true });
        //            state.Cities.Add(new City { Cod = 219, Name = "CIÉNAGA", Status = true });
        //            state.Cities.Add(new City { Cod = 232, Name = "CONCORDIA", Status = true });
        //            state.Cities.Add(new City { Cod = 287, Name = "EL BANCO", Status = true });
        //            state.Cities.Add(new City { Cod = 313, Name = "EL PIÑON", Status = true });
        //            state.Cities.Add(new City { Cod = 316, Name = "EL RETÉN", Status = true });
        //            state.Cities.Add(new City { Cod = 352, Name = "FUNDACIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 393, Name = "GUAMAL", Status = true });
        //            state.Cities.Add(new City { Cod = 605, Name = "NUEVA GRANADA", Status = true });
        //            state.Cities.Add(new City { Cod = 653, Name = "PEDRAZA", Status = true });
        //            state.Cities.Add(new City { Cod = 665, Name = "PIJIÑO", Status = true });
        //            state.Cities.Add(new City { Cod = 672, Name = "PIVIJAY", Status = true });
        //            state.Cities.Add(new City { Cod = 675, Name = "PLATO", Status = true });
        //            state.Cities.Add(new City { Cod = 690, Name = "PUEBLOVIEJO", Status = true });
        //            state.Cities.Add(new City { Cod = 739, Name = "REMOLINO", Status = true });
        //            state.Cities.Add(new City { Cod = 771, Name = "SABANAS DE SAN ANGEL (SAN ANGEL)", Status = true });
        //            state.Cities.Add(new City { Cod = 777, Name = "SALAMINA", Status = true });
        //            state.Cities.Add(new City { Cod = 860, Name = "SAN SEBASTIÁN DE BUENAVISTA", Status = true });
        //            state.Cities.Add(new City { Cod = 864, Name = "SAN ZENÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 866, Name = "SANTA ANA", Status = true });
        //            state.Cities.Add(new City { Cod = 870, Name = "SANTA BÁRBARA DE PINTO", Status = true });
        //            state.Cities.Add(new City { Cod = 877, Name = "SANTA MARTA", Status = true });
        //            state.Cities.Add(new City { Cod = 917, Name = "SITIONUEVO", Status = true });
        //            state.Cities.Add(new City { Cod = 973, Name = "TENERIFE", Status = true });
        //            state.Cities.Add(new City { Cod = 1090, Name = "ZAPAYÁN (PUNTA DE PIEDRAS)", Status = true });
        //            state.Cities.Add(new City { Cod = 1096, Name = "ZONA BANANERA (PRADO - SEVILLA)", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 50, Name = "META", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 2, Name = "ACACÍAS", Status = true });
        //            state.Cities.Add(new City { Cod = 84, Name = "BARRANCA DE UPÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 133, Name = "CABUYARO", Status = true });
        //            state.Cities.Add(new City { Cod = 176, Name = "CASTILLA LA NUEVA", Status = true });
        //            state.Cities.Add(new City { Cod = 253, Name = "CUBARRAL", Status = true });
        //            state.Cities.Add(new City { Cod = 259, Name = "CUMARAL", Status = true });
        //            state.Cities.Add(new City { Cod = 289, Name = "EL CALVARIO", Status = true });
        //            state.Cities.Add(new City { Cod = 294, Name = "EL CASTILLO", Status = true });
        //            state.Cities.Add(new City { Cod = 301, Name = "EL DORADO", Status = true });
        //            state.Cities.Add(new City { Cod = 351, Name = "FUENTE DE ORO", Status = true });
        //            state.Cities.Add(new City { Cod = 379, Name = "GRANADA", Status = true });
        //            state.Cities.Add(new City { Cod = 394, Name = "GUAMAL", Status = true });
        //            state.Cities.Add(new City { Cod = 469, Name = "LA MACARENA", Status = true });
        //            state.Cities.Add(new City { Cod = 501, Name = "LEJANÍAS", Status = true });
        //            state.Cities.Add(new City { Cod = 536, Name = "MAPIRIPAN", Status = true });
        //            state.Cities.Add(new City { Cod = 554, Name = "MESETAS", Status = true });
        //            state.Cities.Add(new City { Cod = 699, Name = "PUERTO CONCORDIA", Status = true });
        //            state.Cities.Add(new City { Cod = 701, Name = "PUERTO GAITÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 705, Name = "PUERTO LLERAS", Status = true });
        //            state.Cities.Add(new City { Cod = 706, Name = "PUERTO LÓPEZ", Status = true });
        //            state.Cities.Add(new City { Cod = 711, Name = "PUERTO RICO", Status = true });
        //            state.Cities.Add(new City { Cod = 741, Name = "RESTREPO", Status = true });
        //            state.Cities.Add(new City { Cod = 802, Name = "SAN CARLOS DE GUAROA", Status = true });
        //            state.Cities.Add(new City { Cod = 826, Name = "SAN JUAN DE ARAMA", Status = true });
        //            state.Cities.Add(new City { Cod = 833, Name = "SAN JUANITO", Status = true });
        //            state.Cities.Add(new City { Cod = 841, Name = "SAN MARTÍN", Status = true });
        //            state.Cities.Add(new City { Cod = 1032, Name = "URIBE", Status = true });
        //            state.Cities.Add(new City { Cod = 1070, Name = "VILLAVICENCIO", Status = true });
        //            state.Cities.Add(new City { Cod = 1075, Name = "VISTA HERMOSA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 52, Name = "NARIÑO", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 18, Name = "ALBÁN (SAN JOSÉ)", Status = true });
        //            state.Cities.Add(new City { Cod = 34, Name = "ANCUYA", Status = true });
        //            state.Cities.Add(new City { Cod = 55, Name = "ARBOLEDA (BERRUECOS)", Status = true });
        //            state.Cities.Add(new City { Cod = 80, Name = "BARBACOAS", Status = true });
        //            state.Cities.Add(new City { Cod = 95, Name = "BELÉN", Status = true });
        //            state.Cities.Add(new City { Cod = 126, Name = "BUESACO", Status = true });
        //            state.Cities.Add(new City { Cod = 184, Name = "CHACHAGUÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 228, Name = "COLÓN (GÉNOVA)", Status = true });
        //            state.Cities.Add(new City { Cod = 235, Name = "CONSACA", Status = true });
        //            state.Cities.Add(new City { Cod = 236, Name = "CONTADERO", Status = true });
        //            state.Cities.Add(new City { Cod = 252, Name = "CUASPUD (CARLOSAMA)", Status = true });
        //            state.Cities.Add(new City { Cod = 261, Name = "CUMBAL", Status = true });
        //            state.Cities.Add(new City { Cod = 262, Name = "CUMBITARA", Status = true });
        //            state.Cities.Add(new City { Cod = 274, Name = "CÓRDOBA", Status = true });
        //            state.Cities.Add(new City { Cod = 296, Name = "EL CHARCO", Status = true });
        //            state.Cities.Add(new City { Cod = 309, Name = "EL PEÑOL", Status = true });
        //            state.Cities.Add(new City { Cod = 319, Name = "EL ROSARIO", Status = true });
        //            state.Cities.Add(new City { Cod = 320, Name = "EL TABLÓN DE GÓMEZ", Status = true });
        //            state.Cities.Add(new City { Cod = 322, Name = "EL TAMBO", Status = true });
        //            state.Cities.Add(new City { Cod = 347, Name = "FRANCISCO PIZARRO", Status = true });
        //            state.Cities.Add(new City { Cod = 353, Name = "FUNES", Status = true });
        //            state.Cities.Add(new City { Cod = 383, Name = "GUACHAVÉS", Status = true });
        //            state.Cities.Add(new City { Cod = 386, Name = "GUACHUCAL", Status = true });
        //            state.Cities.Add(new City { Cod = 391, Name = "GUAITARILLA", Status = true });
        //            state.Cities.Add(new City { Cod = 392, Name = "GUALMATÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 430, Name = "ILES", Status = true });
        //            state.Cities.Add(new City { Cod = 431, Name = "IMÚES", Status = true });
        //            state.Cities.Add(new City { Cod = 434, Name = "IPIALES", Status = true });
        //            state.Cities.Add(new City { Cod = 459, Name = "LA CRUZ", Status = true });
        //            state.Cities.Add(new City { Cod = 464, Name = "LA FLORIDA", Status = true });
        //            state.Cities.Add(new City { Cod = 468, Name = "LA LLANADA", Status = true });
        //            state.Cities.Add(new City { Cod = 484, Name = "LA TOLA", Status = true });
        //            state.Cities.Add(new City { Cod = 486, Name = "LA UNIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 500, Name = "LEIVA", Status = true });
        //            state.Cities.Add(new City { Cod = 505, Name = "LINARES", Status = true });
        //            state.Cities.Add(new City { Cod = 523, Name = "MAGÜI (PAYÁN)", Status = true });
        //            state.Cities.Add(new City { Cod = 528, Name = "MALLAMA (PIEDRANCHA)", Status = true });
        //            state.Cities.Add(new City { Cod = 580, Name = "MOSQUERA", Status = true });
        //            state.Cities.Add(new City { Cod = 591, Name = "NARIÑO", Status = true });
        //            state.Cities.Add(new City { Cod = 616, Name = "OLAYA HERRERA", Status = true });
        //            state.Cities.Add(new City { Cod = 622, Name = "OSPINA", Status = true });
        //            state.Cities.Add(new City { Cod = 676, Name = "POLICARPA", Status = true });
        //            state.Cities.Add(new City { Cod = 681, Name = "POTOSÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 684, Name = "PROVIDENCIA", Status = true });
        //            state.Cities.Add(new City { Cod = 692, Name = "PUERRES", Status = true });
        //            state.Cities.Add(new City { Cod = 719, Name = "PUPIALES", Status = true });
        //            state.Cities.Add(new City { Cod = 745, Name = "RICAURTE", Status = true });
        //            state.Cities.Add(new City { Cod = 752, Name = "ROBERTO PAYÁN (SAN JOSÉ)", Status = true });
        //            state.Cities.Add(new City { Cod = 783, Name = "SAMANIEGO", Status = true });
        //            state.Cities.Add(new City { Cod = 797, Name = "SAN BERNARDO", Status = true });
        //            state.Cities.Add(new City { Cod = 829, Name = "SAN JUAN DE PASTO", Status = true });
        //            state.Cities.Add(new City { Cod = 834, Name = "SAN LORENZO", Status = true });
        //            state.Cities.Add(new City { Cod = 849, Name = "SAN PABLO", Status = true });
        //            state.Cities.Add(new City { Cod = 854, Name = "SAN PEDRO DE CARTAGO", Status = true });
        //            state.Cities.Add(new City { Cod = 865, Name = "SANDONÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 869, Name = "SANTA BÁRBARA (ISCUANDÉ)", Status = true });
        //            state.Cities.Add(new City { Cod = 896, Name = "SAPUYES", Status = true });
        //            state.Cities.Add(new City { Cod = 936, Name = "SOTOMAYOR (LOS ANDES)", Status = true });
        //            state.Cities.Add(new City { Cod = 962, Name = "TAMINANGO", Status = true });
        //            state.Cities.Add(new City { Cod = 963, Name = "TANGUA", Status = true });
        //            state.Cities.Add(new City { Cod = 1012, Name = "TUMACO", Status = true });
        //            state.Cities.Add(new City { Cod = 1023, Name = "TÚQUERRES", Status = true });
        //            state.Cities.Add(new City { Cod = 1079, Name = "YACUANQUER", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 54, Name = "NORTE DE SANTANDER", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 56, Name = "ARBOLEDAS", Status = true });
        //            state.Cities.Add(new City { Cod = 106, Name = "BOCHALEMA", Status = true });
        //            state.Cities.Add(new City { Cod = 119, Name = "BUCARASICA", Status = true });
        //            state.Cities.Add(new City { Cod = 196, Name = "CHINÁCOTA", Status = true });
        //            state.Cities.Add(new City { Cod = 204, Name = "CHITAGÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 238, Name = "CONVENCIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 257, Name = "CUCUTILLA", Status = true });
        //            state.Cities.Add(new City { Cod = 268, Name = "CÁCHIRA", Status = true });
        //            state.Cities.Add(new City { Cod = 269, Name = "CÁCOTA", Status = true });
        //            state.Cities.Add(new City { Cod = 275, Name = "CÚCUTA", Status = true });
        //            state.Cities.Add(new City { Cod = 284, Name = "DURANIA", Status = true });
        //            state.Cities.Add(new City { Cod = 290, Name = "EL CARMEN", Status = true });
        //            state.Cities.Add(new City { Cod = 323, Name = "EL TARRA", Status = true });
        //            state.Cities.Add(new City { Cod = 324, Name = "EL ZULIA", Status = true });
        //            state.Cities.Add(new City { Cod = 376, Name = "GRAMALOTE", Status = true });
        //            state.Cities.Add(new City { Cod = 417, Name = "HACARÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 423, Name = "HERRÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 462, Name = "LA ESPERANZA", Status = true });
        //            state.Cities.Add(new City { Cod = 479, Name = "LA PLAYA", Status = true });
        //            state.Cities.Add(new City { Cod = 496, Name = "LABATECA", Status = true });
        //            state.Cities.Add(new City { Cod = 510, Name = "LOS PATIOS", Status = true });
        //            state.Cities.Add(new City { Cod = 512, Name = "LOURDES", Status = true });
        //            state.Cities.Add(new City { Cod = 586, Name = "MUTISCUA", Status = true });
        //            state.Cities.Add(new City { Cod = 612, Name = "OCAÑA", Status = true });
        //            state.Cities.Add(new City { Cod = 642, Name = "PAMPLONA", Status = true });
        //            state.Cities.Add(new City { Cod = 643, Name = "PAMPLONITA", Status = true });
        //            state.Cities.Add(new City { Cod = 714, Name = "PUERTO SANTANDER", Status = true });
        //            state.Cities.Add(new City { Cod = 734, Name = "RAGONVALIA", Status = true });
        //            state.Cities.Add(new City { Cod = 778, Name = "SALAZAR", Status = true });
        //            state.Cities.Add(new City { Cod = 799, Name = "SAN CALIXTO", Status = true });
        //            state.Cities.Add(new City { Cod = 804, Name = "SAN CAYETANO", Status = true });
        //            state.Cities.Add(new City { Cod = 890, Name = "SANTIAGO", Status = true });
        //            state.Cities.Add(new City { Cod = 898, Name = "SARDINATA", Status = true });
        //            state.Cities.Add(new City { Cod = 908, Name = "SILOS", Status = true });
        //            state.Cities.Add(new City { Cod = 976, Name = "TEORAMA", Status = true });
        //            state.Cities.Add(new City { Cod = 983, Name = "TIBÚ", Status = true });
        //            state.Cities.Add(new City { Cod = 997, Name = "TOLEDO", Status = true });
        //            state.Cities.Add(new City { Cod = 1056, Name = "VILLA CARO", Status = true });
        //            state.Cities.Add(new City { Cod = 1059, Name = "VILLA DEL ROSARIO", Status = true });
        //            state.Cities.Add(new City { Cod = 1097, Name = "ÁBREGO", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 63, Name = "QUINDIO", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 66, Name = "ARMENIA", Status = true });
        //            state.Cities.Add(new City { Cod = 123, Name = "BUENAVISTA", Status = true });
        //            state.Cities.Add(new City { Cod = 143, Name = "CALARCÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 216, Name = "CIRCASIA", Status = true });
        //            state.Cities.Add(new City { Cod = 241, Name = "CORDOBÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 335, Name = "FILANDIA", Status = true });
        //            state.Cities.Add(new City { Cod = 415, Name = "GÉNOVA", Status = true });
        //            state.Cities.Add(new City { Cod = 483, Name = "LA TEBAIDA", Status = true });
        //            state.Cities.Add(new City { Cod = 572, Name = "MONTENEGRO", Status = true });
        //            state.Cities.Add(new City { Cod = 664, Name = "PIJAO", Status = true });
        //            state.Cities.Add(new City { Cod = 730, Name = "QUIMBAYA", Status = true });
        //            state.Cities.Add(new City { Cod = 780, Name = "SALENTO", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 66, Name = "RISARALDA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 47, Name = "APÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 77, Name = "BALBOA", Status = true });
        //            state.Cities.Add(new City { Cod = 97, Name = "BELÉN DE UMBRÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 282, Name = "DOS QUEBRADAS", Status = true });
        //            state.Cities.Add(new City { Cod = 412, Name = "GUÁTICA", Status = true });
        //            state.Cities.Add(new City { Cod = 458, Name = "LA CELIA", Status = true });
        //            state.Cities.Add(new City { Cod = 495, Name = "LA VIRGINIA", Status = true });
        //            state.Cities.Add(new City { Cod = 543, Name = "MARSELLA", Status = true });
        //            state.Cities.Add(new City { Cod = 559, Name = "MISTRATÓ", Status = true });
        //            state.Cities.Add(new City { Cod = 657, Name = "PEREIRA", Status = true });
        //            state.Cities.Add(new City { Cod = 688, Name = "PUEBLO RICO", Status = true });
        //            state.Cities.Add(new City { Cod = 731, Name = "QUINCHÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 882, Name = "SANTA ROSA DE CABAL", Status = true });
        //            state.Cities.Add(new City { Cod = 895, Name = "SANTUARIO", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 68, Name = "SANTANDER", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 9, Name = "AGUADA", Status = true });
        //            state.Cities.Add(new City { Cod = 16, Name = "ALBANIA", Status = true });
        //            state.Cities.Add(new City { Cod = 51, Name = "ARATOCA", Status = true });
        //            state.Cities.Add(new City { Cod = 82, Name = "BARBOSA", Status = true });
        //            state.Cities.Add(new City { Cod = 83, Name = "BARICHARA", Status = true });
        //            state.Cities.Add(new City { Cod = 85, Name = "BARRANCABERMEJA", Status = true });
        //            state.Cities.Add(new City { Cod = 103, Name = "BETULIA", Status = true });
        //            state.Cities.Add(new City { Cod = 112, Name = "BOLÍVAR", Status = true });
        //            state.Cities.Add(new City { Cod = 118, Name = "BUCARAMANGA", Status = true });
        //            state.Cities.Add(new City { Cod = 132, Name = "CABRERA", Status = true });
        //            state.Cities.Add(new City { Cod = 147, Name = "CALIFORNIA", Status = true });
        //            state.Cities.Add(new City { Cod = 161, Name = "CAPITANEJO", Status = true });
        //            state.Cities.Add(new City { Cod = 164, Name = "CARCASÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 179, Name = "CEPITA", Status = true });
        //            state.Cities.Add(new City { Cod = 182, Name = "CERRITO", Status = true });
        //            state.Cities.Add(new City { Cod = 188, Name = "CHARALÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 189, Name = "CHARTA", Status = true });
        //            state.Cities.Add(new City { Cod = 191, Name = "CHIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 199, Name = "CHIPATÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 215, Name = "CIMITARRA", Status = true });
        //            state.Cities.Add(new City { Cod = 230, Name = "CONCEPCIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 234, Name = "CONFINES", Status = true });
        //            state.Cities.Add(new City { Cod = 237, Name = "CONTRATACIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 243, Name = "COROMORO", Status = true });
        //            state.Cities.Add(new City { Cod = 265, Name = "CURITÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 291, Name = "EL CARMEN", Status = true });
        //            state.Cities.Add(new City { Cod = 304, Name = "EL GUACAMAYO", Status = true });
        //            state.Cities.Add(new City { Cod = 311, Name = "EL PEÑON", Status = true });
        //            state.Cities.Add(new City { Cod = 314, Name = "EL PLAYÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 327, Name = "ENCINO", Status = true });
        //            state.Cities.Add(new City { Cod = 328, Name = "ENCISO", Status = true });
        //            state.Cities.Add(new City { Cod = 342, Name = "FLORIDABLANCA", Status = true });
        //            state.Cities.Add(new City { Cod = 343, Name = "FLORIÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 364, Name = "GALÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 374, Name = "GIRÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 380, Name = "GUACA", Status = true });
        //            state.Cities.Add(new City { Cod = 389, Name = "GUADALUPE", Status = true });
        //            state.Cities.Add(new City { Cod = 396, Name = "GUAPOTA", Status = true });
        //            state.Cities.Add(new City { Cod = 405, Name = "GUAVATÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 409, Name = "GUEPSA", Status = true });
        //            state.Cities.Add(new City { Cod = 413, Name = "GÁMBITA", Status = true });
        //            state.Cities.Add(new City { Cod = 419, Name = "HATO", Status = true });
        //            state.Cities.Add(new City { Cod = 447, Name = "JESÚS MARÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 448, Name = "JORDÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 454, Name = "LA BELLEZA", Status = true });
        //            state.Cities.Add(new City { Cod = 474, Name = "LA PAZ", Status = true });
        //            state.Cities.Add(new City { Cod = 498, Name = "LANDÁZURI", Status = true });
        //            state.Cities.Add(new City { Cod = 499, Name = "LEBRIJA", Status = true });
        //            state.Cities.Add(new City { Cod = 511, Name = "LOS SANTOS", Status = true });
        //            state.Cities.Add(new City { Cod = 518, Name = "MACARAVITA", Status = true });
        //            state.Cities.Add(new City { Cod = 546, Name = "MATANZA", Status = true });
        //            state.Cities.Add(new City { Cod = 562, Name = "MOGOTES", Status = true });
        //            state.Cities.Add(new City { Cod = 563, Name = "MOLAGAVITA", Status = true });
        //            state.Cities.Add(new City { Cod = 588, Name = "MÁLAGA", Status = true });
        //            state.Cities.Add(new City { Cod = 611, Name = "OCAMONTE", Status = true });
        //            state.Cities.Add(new City { Cod = 613, Name = "OIBA", Status = true });
        //            state.Cities.Add(new City { Cod = 617, Name = "ONZAGA", Status = true });
        //            state.Cities.Add(new City { Cod = 636, Name = "PALMAR", Status = true });
        //            state.Cities.Add(new City { Cod = 638, Name = "PALMAS DEL SOCORRO", Status = true });
        //            state.Cities.Add(new City { Cod = 661, Name = "PIE DE CUESTA", Status = true });
        //            state.Cities.Add(new City { Cod = 666, Name = "PINCHOTE", Status = true });
        //            state.Cities.Add(new City { Cod = 691, Name = "PUENTE NACIONAL", Status = true });
        //            state.Cities.Add(new City { Cod = 709, Name = "PUERTO PARRA", Status = true });
        //            state.Cities.Add(new City { Cod = 717, Name = "PUERTO WILCHES", Status = true });
        //            state.Cities.Add(new City { Cod = 726, Name = "PÁRAMO", Status = true });
        //            state.Cities.Add(new City { Cod = 746, Name = "RIO NEGRO", Status = true });
        //            state.Cities.Add(new City { Cod = 766, Name = "SABANA DE TORRES", Status = true });
        //            state.Cities.Add(new City { Cod = 788, Name = "SAN ANDRÉS", Status = true });
        //            state.Cities.Add(new City { Cod = 794, Name = "SAN BENITO", Status = true });
        //            state.Cities.Add(new City { Cod = 813, Name = "SAN GÍL", Status = true });
        //            state.Cities.Add(new City { Cod = 817, Name = "SAN JOAQUÍN", Status = true });
        //            state.Cities.Add(new City { Cod = 819, Name = "SAN JOSÉ DE MIRANDA", Status = true });
        //            state.Cities.Add(new City { Cod = 844, Name = "SAN MIGUEL", Status = true });
        //            state.Cities.Add(new City { Cod = 863, Name = "SAN VICENTE DEL CHUCURÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 868, Name = "SANTA BÁRBARA", Status = true });
        //            state.Cities.Add(new City { Cod = 874, Name = "SANTA HELENA DEL OPÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 911, Name = "SIMACOTA", Status = true });
        //            state.Cities.Add(new City { Cod = 921, Name = "SOCORRO", Status = true });
        //            state.Cities.Add(new City { Cod = 937, Name = "SUAITA", Status = true });
        //            state.Cities.Add(new City { Cod = 942, Name = "SUCRE", Status = true });
        //            state.Cities.Add(new City { Cod = 947, Name = "SURATÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 1000, Name = "TONA", Status = true });
        //            state.Cities.Add(new City { Cod = 1039, Name = "VALLE DE SAN JOSÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 1052, Name = "VETAS", Status = true });
        //            state.Cities.Add(new City { Cod = 1066, Name = "VILLANUEVA", Status = true });
        //            state.Cities.Add(new City { Cod = 1077, Name = "VÉLEZ", Status = true });
        //            state.Cities.Add(new City { Cod = 1089, Name = "ZAPATOCA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 70, Name = "SUCRE", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 124, Name = "BUENAVISTA", Status = true });
        //            state.Cities.Add(new City { Cod = 137, Name = "CAIMITO", Status = true });
        //            state.Cities.Add(new City { Cod = 186, Name = "CHALÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 226, Name = "COLOSÓ (RICAURTE)", Status = true });
        //            state.Cities.Add(new City { Cod = 244, Name = "COROZAL", Status = true });
        //            state.Cities.Add(new City { Cod = 249, Name = "COVEÑAS", Status = true });
        //            state.Cities.Add(new City { Cod = 317, Name = "EL ROBLE", Status = true });
        //            state.Cities.Add(new City { Cod = 363, Name = "GALERAS (NUEVA GRANADA)", Status = true });
        //            state.Cities.Add(new City { Cod = 398, Name = "GUARANDA", Status = true });
        //            state.Cities.Add(new City { Cod = 487, Name = "LA UNIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 509, Name = "LOS PALMITOS", Status = true });
        //            state.Cities.Add(new City { Cod = 526, Name = "MAJAGUAL", Status = true });
        //            state.Cities.Add(new City { Cod = 578, Name = "MORROA", Status = true });
        //            state.Cities.Add(new City { Cod = 624, Name = "OVEJAS", Status = true });
        //            state.Cities.Add(new City { Cod = 640, Name = "PALMITO", Status = true });
        //            state.Cities.Add(new City { Cod = 785, Name = "SAMPUÉS", Status = true });
        //            state.Cities.Add(new City { Cod = 795, Name = "SAN BENITO ABAD", Status = true });
        //            state.Cities.Add(new City { Cod = 827, Name = "SAN JUAN DE BETULIA", Status = true });
        //            state.Cities.Add(new City { Cod = 839, Name = "SAN MARCOS", Status = true });
        //            state.Cities.Add(new City { Cod = 847, Name = "SAN ONOFRE", Status = true });
        //            state.Cities.Add(new City { Cod = 852, Name = "SAN PEDRO", Status = true });
        //            state.Cities.Add(new City { Cod = 914, Name = "SINCELEJO", Status = true });
        //            state.Cities.Add(new City { Cod = 915, Name = "SINCÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 943, Name = "SUCRE", Status = true });
        //            state.Cities.Add(new City { Cod = 998, Name = "TOLÚ", Status = true });
        //            state.Cities.Add(new City { Cod = 999, Name = "TOLÚ VIEJO", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 73, Name = "TOLIMA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 25, Name = "ALPUJARRA", Status = true });
        //            state.Cities.Add(new City { Cod = 29, Name = "ALVARADO", Status = true });
        //            state.Cities.Add(new City { Cod = 32, Name = "AMBALEMA", Status = true });
        //            state.Cities.Add(new City { Cod = 43, Name = "ANZOÁTEGUI", Status = true });
        //            state.Cities.Add(new City { Cod = 67, Name = "ARMERO (GUAYABAL)", Status = true });
        //            state.Cities.Add(new City { Cod = 70, Name = "ATACO", Status = true });
        //            state.Cities.Add(new City { Cod = 138, Name = "CAJAMARCA", Status = true });
        //            state.Cities.Add(new City { Cod = 166, Name = "CARMEN DE APICALÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 175, Name = "CASABIANCA", Status = true });
        //            state.Cities.Add(new City { Cod = 187, Name = "CHAPARRAL", Status = true });
        //            state.Cities.Add(new City { Cod = 223, Name = "COELLO", Status = true });
        //            state.Cities.Add(new City { Cod = 250, Name = "COYAIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 263, Name = "CUNDAY", Status = true });
        //            state.Cities.Add(new City { Cod = 280, Name = "DOLORES", Status = true });
        //            state.Cities.Add(new City { Cod = 331, Name = "ESPINAL", Status = true });
        //            state.Cities.Add(new City { Cod = 333, Name = "FALAN", Status = true });
        //            state.Cities.Add(new City { Cod = 337, Name = "FLANDES", Status = true });
        //            state.Cities.Add(new City { Cod = 349, Name = "FRESNO", Status = true });
        //            state.Cities.Add(new City { Cod = 395, Name = "GUAMO", Status = true });
        //            state.Cities.Add(new City { Cod = 424, Name = "HERVEO", Status = true });
        //            state.Cities.Add(new City { Cod = 427, Name = "HONDA", Status = true });
        //            state.Cities.Add(new City { Cod = 428, Name = "IBAGUÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 429, Name = "ICONONZO", Status = true });
        //            state.Cities.Add(new City { Cod = 514, Name = "LÉRIDA", Status = true });
        //            state.Cities.Add(new City { Cod = 515, Name = "LÍBANO", Status = true });
        //            state.Cities.Add(new City { Cod = 540, Name = "MARIQUITA", Status = true });
        //            state.Cities.Add(new City { Cod = 552, Name = "MELGAR", Status = true });
        //            state.Cities.Add(new City { Cod = 583, Name = "MURILLO", Status = true });
        //            state.Cities.Add(new City { Cod = 592, Name = "NATAGAIMA", Status = true });
        //            state.Cities.Add(new City { Cod = 621, Name = "ORTEGA", Status = true });
        //            state.Cities.Add(new City { Cod = 641, Name = "PALOCABILDO", Status = true });
        //            state.Cities.Add(new City { Cod = 662, Name = "PIEDRAS", Status = true });
        //            state.Cities.Add(new City { Cod = 673, Name = "PLANADAS", Status = true });
        //            state.Cities.Add(new City { Cod = 683, Name = "PRADO", Status = true });
        //            state.Cities.Add(new City { Cod = 721, Name = "PURIFICACIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 747, Name = "RIOBLANCO", Status = true });
        //            state.Cities.Add(new City { Cod = 754, Name = "RONCESVALLES", Status = true });
        //            state.Cities.Add(new City { Cod = 757, Name = "ROVIRA", Status = true });
        //            state.Cities.Add(new City { Cod = 779, Name = "SALDAÑA", Status = true });
        //            state.Cities.Add(new City { Cod = 792, Name = "SAN ANTONIO", Status = true });
        //            state.Cities.Add(new City { Cod = 835, Name = "SAN LUIS", Status = true });
        //            state.Cities.Add(new City { Cod = 875, Name = "SANTA ISABEL", Status = true });
        //            state.Cities.Add(new City { Cod = 954, Name = "SUÁREZ", Status = true });
        //            state.Cities.Add(new City { Cod = 1040, Name = "VALLE DE SAN JUAN", Status = true });
        //            state.Cities.Add(new City { Cod = 1046, Name = "VENADILLO", Status = true });
        //            state.Cities.Add(new City { Cod = 1062, Name = "VILLAHERMOSA", Status = true });
        //            state.Cities.Add(new City { Cod = 1069, Name = "VILLARRICA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 76, Name = "VALLE DEL CAUCA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 19, Name = "ALCALÁ", Status = true });
        //            state.Cities.Add(new City { Cod = 35, Name = "ANDALUCÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 42, Name = "ANSERMANUEVO", Status = true });
        //            state.Cities.Add(new City { Cod = 62, Name = "ARGELIA", Status = true });
        //            state.Cities.Add(new City { Cod = 113, Name = "BOLÍVAR", Status = true });
        //            state.Cities.Add(new City { Cod = 120, Name = "BUENAVENTURA", Status = true });
        //            state.Cities.Add(new City { Cod = 127, Name = "BUGA", Status = true });
        //            state.Cities.Add(new City { Cod = 128, Name = "BUGALAGRANDE", Status = true });
        //            state.Cities.Add(new City { Cod = 136, Name = "CAICEDONIA", Status = true });
        //            state.Cities.Add(new City { Cod = 148, Name = "CALIMA (DARIÉN)", Status = true });
        //            state.Cities.Add(new City { Cod = 150, Name = "CALÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 157, Name = "CANDELARIA", Status = true });
        //            state.Cities.Add(new City { Cod = 173, Name = "CARTAGO", Status = true });
        //            state.Cities.Add(new City { Cod = 277, Name = "DAGUA", Status = true });
        //            state.Cities.Add(new City { Cod = 288, Name = "EL CAIRO", Status = true });
        //            state.Cities.Add(new City { Cod = 295, Name = "EL CERRITO", Status = true });
        //            state.Cities.Add(new City { Cod = 302, Name = "EL DOVIO", Status = true });
        //            state.Cities.Add(new City { Cod = 325, Name = "EL ÁGUILA", Status = true });
        //            state.Cities.Add(new City { Cod = 341, Name = "FLORIDA", Status = true });
        //            state.Cities.Add(new City { Cod = 370, Name = "GINEBRA", Status = true });
        //            state.Cities.Add(new City { Cod = 382, Name = "GUACARÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 441, Name = "JAMUNDÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 460, Name = "LA CUMBRE", Status = true });
        //            state.Cities.Add(new City { Cod = 488, Name = "LA UNIÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 494, Name = "LA VICTORIA", Status = true });
        //            state.Cities.Add(new City { Cod = 610, Name = "OBANDO", Status = true });
        //            state.Cities.Add(new City { Cod = 639, Name = "PALMIRA", Status = true });
        //            state.Cities.Add(new City { Cod = 682, Name = "PRADERA", Status = true });
        //            state.Cities.Add(new City { Cod = 742, Name = "RESTREPO", Status = true });
        //            state.Cities.Add(new City { Cod = 748, Name = "RIOFRÍO", Status = true });
        //            state.Cities.Add(new City { Cod = 753, Name = "ROLDANILLO", Status = true });
        //            state.Cities.Add(new City { Cod = 853, Name = "SAN PEDRO", Status = true });
        //            state.Cities.Add(new City { Cod = 904, Name = "SEVILLA", Status = true });
        //            state.Cities.Add(new City { Cod = 1004, Name = "TORO", Status = true });
        //            state.Cities.Add(new City { Cod = 1008, Name = "TRUJILLO", Status = true });
        //            state.Cities.Add(new City { Cod = 1011, Name = "TULÚA", Status = true });
        //            state.Cities.Add(new City { Cod = 1027, Name = "ULLOA", Status = true });
        //            state.Cities.Add(new City { Cod = 1051, Name = "VERSALLES", Status = true });
        //            state.Cities.Add(new City { Cod = 1055, Name = "VIJES", Status = true });
        //            state.Cities.Add(new City { Cod = 1086, Name = "YOTOCO", Status = true });
        //            state.Cities.Add(new City { Cod = 1087, Name = "YUMBO", Status = true });
        //            state.Cities.Add(new City { Cod = 1092, Name = "ZARZAL", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 81, Name = "ARAUCA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 52, Name = "ARAUCA", Status = true });
        //            state.Cities.Add(new City { Cod = 53, Name = "ARAUQUITA", Status = true });
        //            state.Cities.Add(new City { Cod = 251, Name = "CRAVO NORTE", Status = true });
        //            state.Cities.Add(new City { Cod = 345, Name = "FORTÚL", Status = true });
        //            state.Cities.Add(new City { Cod = 712, Name = "PUERTO RONDÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 897, Name = "SARAVENA", Status = true });
        //            state.Cities.Add(new City { Cod = 961, Name = "TAME", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 85, Name = "CASANARE", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 11, Name = "AGUAZUL", Status = true });
        //            state.Cities.Add(new City { Cod = 210, Name = "CHÁMEZA", Status = true });
        //            state.Cities.Add(new City { Cod = 420, Name = "HATO COROZAL", Status = true });
        //            state.Cities.Add(new City { Cod = 481, Name = "LA SALINA", Status = true });
        //            state.Cities.Add(new City { Cod = 535, Name = "MANÍ", Status = true });
        //            state.Cities.Add(new City { Cod = 574, Name = "MONTERREY", Status = true });
        //            state.Cities.Add(new City { Cod = 607, Name = "NUNCHÍA", Status = true });
        //            state.Cities.Add(new City { Cod = 620, Name = "OROCUÉ", Status = true });
        //            state.Cities.Add(new City { Cod = 651, Name = "PAZ DE ARIPORO", Status = true });
        //            state.Cities.Add(new City { Cod = 680, Name = "PORE", Status = true });
        //            state.Cities.Add(new City { Cod = 736, Name = "RECETOR", Status = true });
        //            state.Cities.Add(new City { Cod = 770, Name = "SABANALARGA", Status = true });
        //            state.Cities.Add(new City { Cod = 838, Name = "SAN LUÍS DE PALENQUE", Status = true });
        //            state.Cities.Add(new City { Cod = 955, Name = "SÁCAMA", Status = true });
        //            state.Cities.Add(new City { Cod = 969, Name = "TAURAMENA", Status = true });
        //            state.Cities.Add(new City { Cod = 1007, Name = "TRINIDAD", Status = true });
        //            state.Cities.Add(new City { Cod = 1021, Name = "TÁMARA", Status = true });
        //            state.Cities.Add(new City { Cod = 1067, Name = "VILLANUEVA", Status = true });
        //            state.Cities.Add(new City { Cod = 1085, Name = "YOPAL", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 86, Name = "PUTUMAYO", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 227, Name = "COLÓN", Status = true });
        //            state.Cities.Add(new City { Cod = 561, Name = "MOCOA", Status = true });
        //            state.Cities.Add(new City { Cod = 619, Name = "ORITO", Status = true });
        //            state.Cities.Add(new City { Cod = 693, Name = "PUERTO ASÍS", Status = true });
        //            state.Cities.Add(new City { Cod = 696, Name = "PUERTO CAICEDO", Status = true });
        //            state.Cities.Add(new City { Cod = 702, Name = "PUERTO GUZMÁN", Status = true });
        //            state.Cities.Add(new City { Cod = 703, Name = "PUERTO LEGUÍZAMO", Status = true });
        //            state.Cities.Add(new City { Cod = 812, Name = "SAN FRANCISCO", Status = true });
        //            state.Cities.Add(new City { Cod = 845, Name = "SAN MIGUEL", Status = true });
        //            state.Cities.Add(new City { Cod = 891, Name = "SANTIAGO", Status = true });
        //            state.Cities.Add(new City { Cod = 907, Name = "SIBUNDOY", Status = true });
        //            state.Cities.Add(new City { Cod = 1041, Name = "VALLE DEL GUAMUEZ", Status = true });
        //            state.Cities.Add(new City { Cod = 1060, Name = "VILLAGARZÓN", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 88, Name = "ARCHIPIÉLAGO DE SAN ANDRÉS, PROVIDENCIA Y SANTA CATALINA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 685, Name = "PROVIDENCIA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 91, Name = "AMAZONAS", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 503, Name = "LETICIA", Status = true });
        //            state.Cities.Add(new City { Cod = 708, Name = "PUERTO NARIÑO", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 94, Name = "GUAINÍA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 433, Name = "INÍRIDA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 95, Name = "GUAVIARE", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 142, Name = "CALAMAR", Status = true });
        //            state.Cities.Add(new City { Cod = 315, Name = "EL RETORNO", Status = true });
        //            state.Cities.Add(new City { Cod = 557, Name = "MIRAFLORES", Status = true });
        //            state.Cities.Add(new City { Cod = 824, Name = "SAN JOSÉ DEL GUAVIARE", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 97, Name = "VAUPÉS", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 174, Name = "CARURÚ", Status = true });
        //            state.Cities.Add(new City { Cod = 560, Name = "MITÚ", Status = true });
        //            state.Cities.Add(new City { Cod = 964, Name = "TARAIRA", Status = true });
        //            country.States.Add(state);
        //            state = new State { Cod = 99, Name = "VICHADA", Cities = new List<City>(), Status = true };
        //            state.Cities.Add(new City { Cod = 260, Name = "CUMARIBO", Status = true });
        //            state.Cities.Add(new City { Cod = 480, Name = "LA PRIMAVERA", Status = true });
        //            state.Cities.Add(new City { Cod = 697, Name = "PUERTO CARREÑO", Status = true });
        //            state.Cities.Add(new City { Cod = 886, Name = "SANTA ROSALÍA", Status = true });
        //            country.States.Add(state);
        //            _context.Countries.Add(country);
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //}
        private async Task CheckCountriesApiAsync()
        {
            if (!_context.Countries.Any())
            {
                var responseCountries = await _apiService.GetAsync<List<CountryResponse>>("/v1", "/countries");
                if (responseCountries.IsSuccess)
                {
                    var countries = responseCountries.Result!;
                    foreach (var countryResponse in countries)
                    {
                        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            var responseStates = await _apiService.GetAsync<List<StateResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.IsSuccess)
                            {
                                var states = responseStates.Result!;
                                foreach (var stateResponse in states!)
                                {
                                    var state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        var responseCities = await _apiService.GetAsync<List<CityResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.IsSuccess)
                                        {
                                            var cities = responseCities.Result!;
                                            foreach (var cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                var city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
        private async Task CheckCompanysAsync()
        {
            if (!_context.Companies.Any())
            {
                _context.Companies.Add(new Company
                {
                    Id = 0,
                    Name = "INVESA SA",
                    Nit = "890900652-3",
                    City = await _context.Cities.FirstOrDefaultAsync(x => x.Name.Equals("Medellín")),
                    Status = true,
                });
                _context.Companies.Add(new Company
                {
                    Id = 0,
                    Name = "ATI SAS",
                    Nit = "830118667-1",
                    City = await _context.Cities.FirstOrDefaultAsync(x => x.Name.Equals("Duitama")),
                    Status = true,
                });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckProcessesAsync()
        {
            if (!_context.Processes.Any())
            {
                var company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "890900652-3");
                if (company != null)
                {
                    _context.Processes.Add(new Process { Cod = "I-P001", Description = "Gestion Humana", Status = true, CompanyId = company.Id, Company = company });
                    _context.Processes.Add(new Process { Cod = "I-P002", Description = "Planta pinturas", Status = true, CompanyId = company.Id, Company = company });
                    _context.Processes.Add(new Process { Cod = "I-P003", Description = "Planta agroquimicos", Status = true, CompanyId = company.Id, Company = company });
                }
                company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "830118667-1");
                if (company != null)
                {
                    _context.Processes.Add(new Process { Cod = "ATI-P001", Description = "Gestion Humana", Status = true, CompanyId = company.Id, Company = company });
                    _context.Processes.Add(new Process { Cod = "ATI-P002", Description = "HSEQ", Status = true, CompanyId = company.Id, Company = company });
                    _context.Processes.Add(new Process { Cod = "ATI-P003", Description = "Financiera", Status = true, CompanyId = company.Id, Company = company });
                }
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckOccupationsAsync()
        {
            if (!_context.Occupations.Any())
            {
                var process = await _context.Processes.FirstOrDefaultAsync(p => p.Cod.Equals("I-P001"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Cod = "CA001", Description = "Analista Gestion Humana", ProcessId = process.Id, Process = process, Status = true });
                }
                process = await _context.Processes.FirstOrDefaultAsync(p => p.Cod.Equals("I-P002"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Cod = "CA002", Description = "Operario pinturas", ProcessId = process.Id, Process = process, Status = true });
                    _context.Occupations.Add(new Occupation { Cod = "CA003", Description = "Coordinador Pinturas", ProcessId = process.Id, Process = process, Status = true });
                }
                process = await _context.Processes.FirstOrDefaultAsync(p => p.Cod.Equals("I-P003"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Cod = "CA004", Description = "Auxiliar Pinturas", ProcessId = process.Id, Process = process, Status = true });
                    _context.Occupations.Add(new Occupation { Cod = "CA005", Description = "Operario Agroquimicos", ProcessId = process.Id, Process = process, Status = true });
                }
                process = await _context.Processes.FirstOrDefaultAsync(p => p.Cod.Equals("ATI-P001"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Cod = "CA002", Description = "Lider Gestión Humana", ProcessId = process.Id, Process = process, Status = true });
                    _context.Occupations.Add(new Occupation { Cod = "CA003", Description = "Lider Administrativo", ProcessId = process.Id, Process = process, Status = true });
                }
                process = await _context.Processes.FirstOrDefaultAsync(p => p.Cod.Equals("ATI-P002"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Cod = "CA004", Description = "Coordinador Hseq", ProcessId = process.Id, Process = process, Status = true });
                    _context.Occupations.Add(new Occupation { Cod = "CA005", Description = "Coordinador Psev", ProcessId = process.Id, Process = process, Status = true });
                }
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckFormationsAsync()
        {
            if (!_context.Formations.Any())
            {
                var occupation = await _context.Occupations.FirstOrDefaultAsync(o => o.Cod.Equals("CA001"));
                if (occupation != null)
                {
                    var company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "890900652-3");
                    if (company != null)
                    {
                        _context.Formations.Add(new Formation { Cod = "E001", Description = "Inducción del personal.", Status = true, CompanyId = company.Id, Company = company }); ;
                        _context.Formations.Add(new Formation { Cod = "E002", Description = "Formación en seguridad.", Status = true, CompanyId = company.Id, Company = company });
                    }
                    company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "830118667-1");
                    if (company != null)
                    {
                        _context.Formations.Add(new Formation { Cod = "A001", Description = "Riesgo Biológico.", Status = true, CompanyId = company.Id, Company = company }); ;
                        _context.Formations.Add(new Formation { Cod = "A002", Description = "Manejo de Extintores.", Status = true, CompanyId = company.Id, Company = company });
                    }
                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task CheckTopicsAsync()
        {
            if (!_context.Topics.Any())
            {
                var company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "890900652-3");
                if (company != null)
                {
                    _context.Topics.Add(new Topic { Description = "Sistema Integrado de Gestión.", Status = true, CompanyId = company.Id, Company = company });
                    _context.Topics.Add(new Topic { Description = "Medio ambiente", Status = true, CompanyId = company.Id, Company = company });
                }
                company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "830118667-1");
                if (company != null)
                {
                    _context.Topics.Add(new Topic { Description = "Sistema Integrado de Gestión.", Status = true, CompanyId = company.Id, Company = company });
                    _context.Topics.Add(new Topic { Description = "Medio ambiente", Status = true, CompanyId = company.Id, Company = company });
                }
                await _context.SaveChangesAsync();
            }
        }


        //private async Task CheckTrainingCalendarAsync()
        //{
        //    if (!_context.TrainingSessions.Any())
        //    {
        //        _context.TrainingCalendars.Add(new TrainingCalendar { 
        //            Description = "Primer trimestre 2024", 
        //            DateStart = DateTime.Parse("2024-01-01"), 
        //            DateEnd = DateTime.Parse("2024-03-31"), 
        //            Status = true });
        //        _context.TrainingCalendars.Add(new TrainingCalendar
        //        {
        //            Description = "Segundo trimestre 2024",
        //            DateStart = DateTime.Parse("2024-04-01"),
        //            DateEnd = DateTime.Parse("2024-06-30"),
        //            Status = true
        //        });
        //        _context.TrainingCalendars.Add(new TrainingCalendar
        //        {
        //            Description = "Tercer trimestre 2024",
        //            DateStart = DateTime.Parse("2024-07-01"),
        //            DateEnd = DateTime.Parse("2024-09-30"),
        //            Status = true
        //        });
        //        _context.TrainingCalendars.Add(new TrainingCalendar
        //        {
        //            Description = "Tercer trimestre 2024",
        //            DateStart = DateTime.Parse("2024-10-01"),
        //            DateEnd = DateTime.Parse("2024-12-31"),
        //            Status = true
        //        });
        //        await _context.SaveChangesAsync();
        //    }
        //}
        private async Task CheckTrainingsAsync()
        {
            if (!_context.Trainings.Any())
            {
                var process = await _context.Processes.FirstOrDefaultAsync(o => o.Cod.Equals("I-P001"));
                if (process != null)
                {
                    _context.Trainings.Add(new Training { Cod = "ICA001", Description = "Inducción al sistema integrado de gestión.", Type = true, Duration = 60, ProcessId = process.Id, Process = process, Status = true, });
                    _context.Trainings.Add(new Training { Cod = "ICA002", Description = "Manejo integral de residuos solidos.", Type = true, Duration = 60, ProcessId = process.Id, Process = process, Status = true });

                    await _context.SaveChangesAsync();
                }
                process = await _context.Processes.FirstOrDefaultAsync(o => o.Cod.Equals("ATI-P002"));
                if (process != null)
                {
                    _context.Trainings.Add(new Training { Cod = "ATICA001", Description = "Inducción al sistema integrado de gestión.", Type = true, Duration = 60, ProcessId = process.Id, Process = process, Status = true, });
                    _context.Trainings.Add(new Training { Cod = "ATICA002", Description = "Manejo integral de residuos solidos.", Type = true, Duration = 60, ProcessId = process.Id, Process = process, Status = true });

                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task CheckSlider()
        {
            if (!_context.Sliders.Any())
            {
                _context.Sliders.Add(new Slider { Description = "Base", Image = "https://neosmartstorage.blob.core.windows.net/sliders/f2ee58b1-831b-4c0b-97eb-f7859aff5d7c.jpg" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckUserAsync(string? nit, string document, string firstName, string lastName, string email, string phoneNumber, string address, string city, UserType userType, string pass)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Address = address,
                    Document = document,
                    DocumentType = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Name == "CC"),
                    City = await _context.Cities.FirstOrDefaultAsync(x => x.Name.Equals(city)),
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    UserName = email,
                    EmailConfirmed = true
                };
                if (!string.IsNullOrEmpty(nit))
                {
                    var company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == nit);
                    if (company != null)
                    {
                        user.CompanyId = company.Id;
                        user.Company = company;
                    }
                }
                try
                {
                    await _userHelper.AddUserAsync(user, pass);
                    await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                    //string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    //await _userHelper.ConfirmEmailAsync(user, token);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
