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

            await CheckCountriesAsync();
            await CheckResourceTypesAsync();
            await CheckDocumentTypesAsync();
            await CheckCompanysAsync();
            await CheckProcessesAsync();
            await CheckOccupationsAsync();
            await CheckFormationsAsync();
            await CheckTopicsAsync();
            await CheckRequestStatusAsync();
            await CheckSessionStatusAsync();
            await CheckTrainingsAsync();
            await CheckRolesAsycn();
            await CheckSlider();
            await CheckSessionInscriptionStatusAsync();
            await CheckUserAsync(null, "1090388348", "Daniel", "Oicata Hernandez", "danieloicata1125413@correo.itm.edu.co", "3177457755", "CARRERA", "DUITAMA", null, UserType.SuperAdmin, "1090Jeep$");
            await CheckUserAsync(null, "43993064", "Elizabet", "Loaiza Garcia", "elizabetloaiza1125440@correo.itm.edu.co", "3104995761", "CARRERA", "MEDELLÍN", null, UserType.SuperAdmin, "Inicio123*");
            await CheckUserAsync(null, "15374665", "Henry Alonso", "Muñoz Carvajal", "henrymunoz1125401@correo.itm.edu.co", "3218399637", "CARRERA", "MEDELLÍN", null, UserType.SuperAdmin, "Inicio123*");
            await CheckUserAsync("890900652-3", "12345", "Gerente", "Interno", "gerente@neosmart.com.co", null, "CARRERA", "MEDELLÍN", null, UserType.Admin, "Inicio123*");
            await CheckUserAsync("890900652-3", "123456", "Coordinador", "Interno", "coordinador@neosmart.com.co", null, "CARRERA", "MEDELLÍN", null, UserType.Manager, "Inicio123*");
            await CheckUserAsync("890900652-3", "1234567", "Lider", "Interno", "lider@neosmart.com.co", null, "CARRERA", "MEDELLÍN", null, UserType.Leader, "Inicio123*");
            await CheckUserAsync("890900652-3", "12345678", "Capacitador", "Interno", "capacitador@neosmart.com.co", null, "CARRERA", "MEDELLÍN", null, UserType.Trainer, "Inicio123*");
            await CheckUserAsync("890900652-3", "23520955", "Trabajador", "Interno", "trabajador@neosmart.com.co", "3177457755", "CARRERA", "MEDELLÍN", null, UserType.Employee, "Inicio123*");
            await CheckUseCompanysrAsync();

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
        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == "COLOMBIA")!;
                if (country == null)
                {
                    country = new() { Name = "COLOMBIA", States = new List<State>() };
                    var state = new State() { Name = "ANTIOQUIA", Cities = new List<City>() };
                    state.Cities.Add(new City { Name = "MEDELLÍN" });
                    country.States.Add(state);
                    state = new State() { Name = "BOYACÁ", Cities = new List<City>() };
                    state.Cities.Add(new City { Name = "DUITAMA" });
                    country.States.Add(state);
                    _context.Countries.Add(country);
                    await _context.SaveChangesAsync();
                }
                //if (country == null)
                //{
                //    country = new() { Name = "COLOMBIA", States = new List<State>() };
                //    var state = new State() { Name = "ANTIOQUIA", Cities = new List<City>() };
                //    state.Cities.Add(new City() { Name = "ABRIAQUÍ" });
                //    state.Cities.Add(new City { Name = "ALEJANDRIA" });
                //    state.Cities.Add(new City { Name = "AMAGÁ" });
                //    state.Cities.Add(new City { Name = "AMALFI" });
                //    state.Cities.Add(new City { Name = "ANDES" });
                //    state.Cities.Add(new City { Name = "ANGELÓPOLIS" });
                //    state.Cities.Add(new City { Name = "ANGOSTURA" });
                //    state.Cities.Add(new City { Name = "ANORÍ" });
                //    state.Cities.Add(new City { Name = "ANZÁ" });
                //    state.Cities.Add(new City { Name = "APARTADÓ" });
                //    state.Cities.Add(new City { Name = "ARBOLETES" });
                //    state.Cities.Add(new City { Name = "ARGELIA" });
                //    state.Cities.Add(new City { Name = "ARMENIA" });
                //    state.Cities.Add(new City { Name = "BARBOSA" });
                //    state.Cities.Add(new City { Name = "BELLO" });
                //    state.Cities.Add(new City { Name = "BELMIRA" });
                //    state.Cities.Add(new City { Name = "BETANIA" });
                //    state.Cities.Add(new City { Name = "BETULIA" });
                //    state.Cities.Add(new City { Name = "BOLÍVAR" });
                //    state.Cities.Add(new City { Name = "BRICEÑO" });
                //    state.Cities.Add(new City { Name = "BURÍTICA" });
                //    state.Cities.Add(new City { Name = "CAICEDO" });
                //    state.Cities.Add(new City { Name = "CALDAS" });
                //    state.Cities.Add(new City { Name = "CAMPAMENTO" });
                //    state.Cities.Add(new City { Name = "CARACOLÍ" });
                //    state.Cities.Add(new City { Name = "CARAMANTA" });
                //    state.Cities.Add(new City { Name = "CAREPA" });
                //    state.Cities.Add(new City { Name = "CARMEN DE VIBORAL" });
                //    state.Cities.Add(new City { Name = "CAROLINA" });
                //    state.Cities.Add(new City { Name = "CAUCASIA" });
                //    state.Cities.Add(new City { Name = "CAÑASGORDAS" });
                //    state.Cities.Add(new City { Name = "CHIGORODÓ" });
                //    state.Cities.Add(new City { Name = "CISNEROS" });
                //    state.Cities.Add(new City { Name = "COCORNÁ" });
                //    state.Cities.Add(new City { Name = "CONCEPCIÓN" });
                //    state.Cities.Add(new City { Name = "CONCORDIA" });
                //    state.Cities.Add(new City { Name = "COPACABANA" });
                //    state.Cities.Add(new City { Name = "CÁCERES" });
                //    state.Cities.Add(new City { Name = "DABEIBA" });
                //    state.Cities.Add(new City { Name = "DON MATÍAS" });
                //    state.Cities.Add(new City { Name = "EBÉJICO" });
                //    state.Cities.Add(new City { Name = "EL BAGRE" });
                //    state.Cities.Add(new City { Name = "ENTRERRÍOS" });
                //    state.Cities.Add(new City { Name = "ENVIGADO" });
                //    state.Cities.Add(new City { Name = "FREDONIA" });
                //    state.Cities.Add(new City { Name = "FRONTINO" });
                //    state.Cities.Add(new City { Name = "GIRALDO" });
                //    state.Cities.Add(new City { Name = "GIRARDOTA" });
                //    state.Cities.Add(new City { Name = "GRANADA" });
                //    state.Cities.Add(new City { Name = "GUADALUPE" });
                //    state.Cities.Add(new City { Name = "GUARNE" });
                //    state.Cities.Add(new City { Name = "GUATAPÉ" });
                //    state.Cities.Add(new City { Name = "GÓMEZ PLATA" });
                //    state.Cities.Add(new City { Name = "HELICONIA" });
                //    state.Cities.Add(new City { Name = "HISPANIA" });
                //    state.Cities.Add(new City { Name = "ITAGÜÍ" });
                //    state.Cities.Add(new City { Name = "ITUANGO" });
                //    state.Cities.Add(new City { Name = "JARDÍN" });
                //    state.Cities.Add(new City { Name = "JERICÓ" });
                //    state.Cities.Add(new City { Name = "LA CEJA" });
                //    state.Cities.Add(new City { Name = "LA ESTRELLA" });
                //    state.Cities.Add(new City { Name = "LA PINTADA" });
                //    state.Cities.Add(new City { Name = "LA UNIÓN" });
                //    state.Cities.Add(new City { Name = "LIBORINA" });
                //    state.Cities.Add(new City { Name = "MACEO" });
                //    state.Cities.Add(new City { Name = "MARINILLA" });
                //    state.Cities.Add(new City { Name = "MEDELLÍN" });
                //    state.Cities.Add(new City { Name = "MONTEBELLO" });
                //    state.Cities.Add(new City { Name = "MURINDÓ" });
                //    state.Cities.Add(new City { Name = "MUTATÁ" });
                //    state.Cities.Add(new City { Name = "NARIÑO" });
                //    state.Cities.Add(new City { Name = "NECHÍ" });
                //    state.Cities.Add(new City { Name = "NECOCLÍ" });
                //    state.Cities.Add(new City { Name = "OLAYA" });
                //    state.Cities.Add(new City { Name = "PEQUE" });
                //    state.Cities.Add(new City { Name = "PEÑOL" });
                //    state.Cities.Add(new City { Name = "PUEBLORRICO" });
                //    state.Cities.Add(new City { Name = "PUERTO BERRÍO" });
                //    state.Cities.Add(new City { Name = "PUERTO NARE" });
                //    state.Cities.Add(new City { Name = "PUERTO TRIUNFO" });
                //    state.Cities.Add(new City { Name = "REMEDIOS" });
                //    state.Cities.Add(new City { Name = "RETIRO" });
                //    state.Cities.Add(new City { Name = "RÍONEGRO" });
                //    state.Cities.Add(new City { Name = "SABANALARGA" });
                //    state.Cities.Add(new City { Name = "SABANETA" });
                //    state.Cities.Add(new City { Name = "SALGAR" });
                //    state.Cities.Add(new City { Name = "SAN ANDRÉS DE CUERQUÍA" });
                //    state.Cities.Add(new City { Name = "SAN CARLOS" });
                //    state.Cities.Add(new City { Name = "SAN FRANCISCO" });
                //    state.Cities.Add(new City { Name = "SAN JERÓNIMO" });
                //    state.Cities.Add(new City { Name = "SAN JOSÉ DE MONTAÑA" });
                //    state.Cities.Add(new City { Name = "SAN JUAN DE URABÁ" });
                //    state.Cities.Add(new City { Name = "SAN LUÍS" });
                //    state.Cities.Add(new City { Name = "SAN PEDRO" });
                //    state.Cities.Add(new City { Name = "SAN PEDRO DE URABÁ" });
                //    state.Cities.Add(new City { Name = "SAN RAFAEL" });
                //    state.Cities.Add(new City { Name = "SAN ROQUE" });
                //    state.Cities.Add(new City { Name = "SAN VICENTE" });
                //    state.Cities.Add(new City { Name = "SANTA BÁRBARA" });
                //    state.Cities.Add(new City { Name = "SANTA FÉ DE ANTIOQUIA" });
                //    state.Cities.Add(new City { Name = "SANTA ROSA DE OSOS" });
                //    state.Cities.Add(new City { Name = "SANTO DOMINGO" });
                //    state.Cities.Add(new City { Name = "SANTUARIO" });
                //    state.Cities.Add(new City { Name = "SEGOVIA" });
                //    state.Cities.Add(new City { Name = "SONSÓN" });
                //    state.Cities.Add(new City { Name = "SOPETRÁN" });
                //    state.Cities.Add(new City { Name = "TARAZÁ" });
                //    state.Cities.Add(new City { Name = "TARSO" });
                //    state.Cities.Add(new City { Name = "TITIRIBÍ" });
                //    state.Cities.Add(new City { Name = "TOLEDO" });
                //    state.Cities.Add(new City { Name = "TURBO" });
                //    state.Cities.Add(new City { Name = "TÁMESIS" });
                //    state.Cities.Add(new City { Name = "URAMITA" });
                //    state.Cities.Add(new City { Name = "URRAO" });
                //    state.Cities.Add(new City { Name = "VALDIVIA" });
                //    state.Cities.Add(new City { Name = "VALPARAISO" });
                //    state.Cities.Add(new City { Name = "VEGACHÍ" });
                //    state.Cities.Add(new City { Name = "VENECIA" });
                //    state.Cities.Add(new City { Name = "VIGÍA DEL FUERTE" });
                //    state.Cities.Add(new City { Name = "YALÍ" });
                //    state.Cities.Add(new City { Name = "YARUMAL" });
                //    state.Cities.Add(new City { Name = "YOLOMBÓ" });
                //    state.Cities.Add(new City { Name = "YONDÓ (CASABE)" });
                //    state.Cities.Add(new City { Name = "ZARAGOZA" });
                //    country.States.Add(state);
                //    state = new State() { Cod = 8, Name = "ATLÁNTICO", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 78, Name = "BARANOA" });
                //    state.Cities.Add(new City { Cod = 88, Name = "BARRANQUILLA" });
                //    state.Cities.Add(new City { Cod = 152, Name = "CAMPO DE LA CRUZ" });
                //    state.Cities.Add(new City { Cod = 156, Name = "CANDELARIA" });
                //    state.Cities.Add(new City { Cod = 362, Name = "GALAPA" });
                //    state.Cities.Add(new City { Cod = 449, Name = "JUAN DE ACOSTA" });
                //    state.Cities.Add(new City { Cod = 513, Name = "LURUACO" });
                //    state.Cities.Add(new City { Cod = 527, Name = "MALAMBO" });
                //    state.Cities.Add(new City { Cod = 529, Name = "MANATÍ" });
                //    state.Cities.Add(new City { Cod = 637, Name = "PALMAR DE VARELA" });
                //    state.Cities.Add(new City { Cod = 668, Name = "PIOJO" });
                //    state.Cities.Add(new City { Cod = 677, Name = "POLONUEVO" });
                //    state.Cities.Add(new City { Cod = 678, Name = "PONEDERA" });
                //    state.Cities.Add(new City { Cod = 698, Name = "PUERTO COLOMBIA" });
                //    state.Cities.Add(new City { Cod = 740, Name = "REPELÓN" });
                //    state.Cities.Add(new City { Cod = 767, Name = "SABANAGRANDE" });
                //    state.Cities.Add(new City { Cod = 769, Name = "SABANALARGA" });
                //    state.Cities.Add(new City { Cod = 876, Name = "SANTA LUCÍA" });
                //    state.Cities.Add(new City { Cod = 893, Name = "SANTO TOMÁS" });
                //    state.Cities.Add(new City { Cod = 925, Name = "SOLEDAD" });
                //    state.Cities.Add(new City { Cod = 938, Name = "SUAN" });
                //    state.Cities.Add(new City { Cod = 1009, Name = "TUBARÁ" });
                //    state.Cities.Add(new City { Cod = 1036, Name = "USIACURI" });
                //    country.States.Add(state);
                //    state = new State { Cod = 11, Name = "BOGOTÁ, D.C.", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 107, Name = "BOGOTÁ D.C." });
                //    country.States.Add(state);
                //    state = new State { Cod = 13, Name = "BOLÍVAR", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 5, Name = "ACHÍ" });
                //    state.Cities.Add(new City { Cod = 28, Name = "ALTOS DEL ROSARIO" });
                //    state.Cities.Add(new City { Cod = 59, Name = "ARENAL" });
                //    state.Cities.Add(new City { Cod = 64, Name = "ARJONA" });
                //    state.Cities.Add(new City { Cod = 68, Name = "ARROYOHONDO" });
                //    state.Cities.Add(new City { Cod = 87, Name = "BARRANCO DE LOBA" });
                //    state.Cities.Add(new City { Cod = 141, Name = "CALAMAR" });
                //    state.Cities.Add(new City { Cod = 158, Name = "CANTAGALLO" });
                //    state.Cities.Add(new City { Cod = 171, Name = "CARTAGENA" });
                //    state.Cities.Add(new City { Cod = 214, Name = "CICUCO" });
                //    state.Cities.Add(new City { Cod = 221, Name = "CLEMENCIA" });
                //    state.Cities.Add(new City { Cod = 273, Name = "CÓRDOBA" });
                //    state.Cities.Add(new City { Cod = 293, Name = "EL CARMEN DE BOLÍVAR" });
                //    state.Cities.Add(new City { Cod = 305, Name = "EL GUAMO" });
                //    state.Cities.Add(new City { Cod = 310, Name = "EL PEÑON" });
                //    state.Cities.Add(new City { Cod = 418, Name = "HATILLO DE LOBA" });
                //    state.Cities.Add(new City { Cod = 522, Name = "MAGANGUÉ" });
                //    state.Cities.Add(new City { Cod = 524, Name = "MAHATES" });
                //    state.Cities.Add(new City { Cod = 537, Name = "MARGARITA" });
                //    state.Cities.Add(new City { Cod = 545, Name = "MARÍA LA BAJA" });
                //    state.Cities.Add(new City { Cod = 565, Name = "MOMPÓS" });
                //    state.Cities.Add(new City { Cod = 570, Name = "MONTECRISTO" });
                //    state.Cities.Add(new City { Cod = 575, Name = "MORALES" });
                //    state.Cities.Add(new City { Cod = 603, Name = "NOROSÍ" });
                //    state.Cities.Add(new City { Cod = 667, Name = "PINILLOS" });
                //    state.Cities.Add(new City { Cod = 737, Name = "REGIDOR" });
                //    state.Cities.Add(new City { Cod = 762, Name = "RÍO VIEJO" });
                //    state.Cities.Add(new City { Cod = 805, Name = "SAN CRISTOBAL" });
                //    state.Cities.Add(new City { Cod = 808, Name = "SAN ESTANISLAO" });
                //    state.Cities.Add(new City { Cod = 809, Name = "SAN FERNANDO" });
                //    state.Cities.Add(new City { Cod = 814, Name = "SAN JACINTO" });
                //    state.Cities.Add(new City { Cod = 815, Name = "SAN JACINTO DEL CAUCA" });
                //    state.Cities.Add(new City { Cod = 828, Name = "SAN JUAN DE NEPOMUCENO" });
                //    state.Cities.Add(new City { Cod = 842, Name = "SAN MARTÍN DE LOBA" });
                //    state.Cities.Add(new City { Cod = 848, Name = "SAN PABLO" });
                //    state.Cities.Add(new City { Cod = 871, Name = "SANTA CATALINA" });
                //    state.Cities.Add(new City { Cod = 880, Name = "SANTA ROSA" });
                //    state.Cities.Add(new City { Cod = 885, Name = "SANTA ROSA DEL SUR" });
                //    state.Cities.Add(new City { Cod = 913, Name = "SIMITÍ" });
                //    state.Cities.Add(new City { Cod = 930, Name = "SOPLAVIENTO" });
                //    state.Cities.Add(new City { Cod = 959, Name = "TALAIGUA NUEVO" });
                //    state.Cities.Add(new City { Cod = 990, Name = "TIQUISIO (PUERTO RICO)" });
                //    state.Cities.Add(new City { Cod = 1015, Name = "TURBACO" });
                //    state.Cities.Add(new City { Cod = 1016, Name = "TURBANÁ" });
                //    state.Cities.Add(new City { Cod = 1064, Name = "VILLANUEVA" });
                //    state.Cities.Add(new City { Cod = 1088, Name = "ZAMBRANO" });
                //    country.States.Add(state);
                //    state = new State { Cod = 15, Name = "BOYACÁ", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 24, Name = "ALMEIDA" });
                //    state.Cities.Add(new City { Cod = 48, Name = "AQUITANIA" });
                //    state.Cities.Add(new City { Cod = 58, Name = "ARCABUCO" });
                //    state.Cities.Add(new City { Cod = 94, Name = "BELÉN" });
                //    state.Cities.Add(new City { Cod = 99, Name = "BERBEO" });
                //    state.Cities.Add(new City { Cod = 101, Name = "BETEITIVA" });
                //    state.Cities.Add(new City { Cod = 105, Name = "BOAVITA" });
                //    state.Cities.Add(new City { Cod = 115, Name = "BOYACÁ" });
                //    state.Cities.Add(new City { Cod = 117, Name = "BRICEÑO" });
                //    state.Cities.Add(new City { Cod = 121, Name = "BUENAVISTA" });
                //    state.Cities.Add(new City { Cod = 130, Name = "BUSBANZA" });
                //    state.Cities.Add(new City { Cod = 145, Name = "CALDAS" });
                //    state.Cities.Add(new City { Cod = 154, Name = "CAMPOHERMOSO" });
                //    state.Cities.Add(new City { Cod = 181, Name = "CERINZA" });
                //    state.Cities.Add(new City { Cod = 194, Name = "CHINAVITA" });
                //    state.Cities.Add(new City { Cod = 200, Name = "CHIQUINQUIRÁ" });
                //    state.Cities.Add(new City { Cod = 202, Name = "CHISCAS" });
                //    state.Cities.Add(new City { Cod = 203, Name = "CHITA" });
                //    state.Cities.Add(new City { Cod = 205, Name = "CHITARAQUE" });
                //    state.Cities.Add(new City { Cod = 206, Name = "CHIVATÁ" });
                //    state.Cities.Add(new City { Cod = 212, Name = "CHÍQUIZA" });
                //    state.Cities.Add(new City { Cod = 213, Name = "CHÍVOR" });
                //    state.Cities.Add(new City { Cod = 218, Name = "CIÉNAGA" });
                //    state.Cities.Add(new City { Cod = 240, Name = "COPER" });
                //    state.Cities.Add(new City { Cod = 245, Name = "CORRALES" });
                //    state.Cities.Add(new City { Cod = 248, Name = "COVARACHÍA" });
                //    state.Cities.Add(new City { Cod = 254, Name = "CUBARÁ" });
                //    state.Cities.Add(new City { Cod = 255, Name = "CUCAITA" });
                //    state.Cities.Add(new City { Cod = 258, Name = "CUITIVA" });
                //    state.Cities.Add(new City { Cod = 272, Name = "CÓMBITA" });
                //    state.Cities.Add(new City { Cod = 283, Name = "DUITAMA" });
                //    state.Cities.Add(new City { Cod = 297, Name = "EL COCUY" });
                //    state.Cities.Add(new City { Cod = 303, Name = "EL ESPINO" });
                //    state.Cities.Add(new City { Cod = 336, Name = "FIRAVITOBA" });
                //    state.Cities.Add(new City { Cod = 340, Name = "FLORESTA" });
                //    state.Cities.Add(new City { Cod = 360, Name = "GACHANTIVÁ" });
                //    state.Cities.Add(new City { Cod = 367, Name = "GARAGOA" });
                //    state.Cities.Add(new City { Cod = 381, Name = "GUACAMAYAS" });
                //    state.Cities.Add(new City { Cod = 404, Name = "GUATEQUE" });
                //    state.Cities.Add(new City { Cod = 408, Name = "GUAYATÁ" });
                //    state.Cities.Add(new City { Cod = 410, Name = "GUICÁN" });
                //    state.Cities.Add(new City { Cod = 414, Name = "GÁMEZA" });
                //    state.Cities.Add(new City { Cod = 439, Name = "IZÁ" });
                //    state.Cities.Add(new City { Cod = 443, Name = "JENESANO" });
                //    state.Cities.Add(new City { Cod = 445, Name = "JERICÓ" });
                //    state.Cities.Add(new City { Cod = 456, Name = "LA CAPILLA" });
                //    state.Cities.Add(new City { Cod = 489, Name = "LA UVITA" });
                //    state.Cities.Add(new City { Cod = 492, Name = "LA VICTORIA" });
                //    state.Cities.Add(new City { Cod = 497, Name = "LABRANZAGRANDE" });
                //    state.Cities.Add(new City { Cod = 517, Name = "MACANAL" });
                //    state.Cities.Add(new City { Cod = 539, Name = "MARIPÍ" });
                //    state.Cities.Add(new City { Cod = 556, Name = "MIRAFLORES" });
                //    state.Cities.Add(new City { Cod = 566, Name = "MONGUA" });
                //    state.Cities.Add(new City { Cod = 567, Name = "MONGUÍ" });
                //    state.Cities.Add(new City { Cod = 568, Name = "MONIQUIRÁ" });
                //    state.Cities.Add(new City { Cod = 581, Name = "MOTAVITA" });
                //    state.Cities.Add(new City { Cod = 587, Name = "MUZO" });
                //    state.Cities.Add(new City { Cod = 600, Name = "NOBSA" });
                //    state.Cities.Add(new City { Cod = 606, Name = "NUEVO COLÓN" });
                //    state.Cities.Add(new City { Cod = 614, Name = "OICATÁ" });
                //    state.Cities.Add(new City { Cod = 623, Name = "OTANCHE" });
                //    state.Cities.Add(new City { Cod = 625, Name = "PACHAVITA" });
                //    state.Cities.Add(new City { Cod = 631, Name = "PAIPA" });
                //    state.Cities.Add(new City { Cod = 632, Name = "PAJARITO" });
                //    state.Cities.Add(new City { Cod = 645, Name = "PANQUEBA" });
                //    state.Cities.Add(new City { Cod = 649, Name = "PAUNA" });
                //    state.Cities.Add(new City { Cod = 650, Name = "PAYA" });
                //    state.Cities.Add(new City { Cod = 652, Name = "PAZ DE RÍO" });
                //    state.Cities.Add(new City { Cod = 658, Name = "PESCA" });
                //    state.Cities.Add(new City { Cod = 669, Name = "PISVA" });
                //    state.Cities.Add(new City { Cod = 695, Name = "PUERTO BOYACÁ" });
                //    state.Cities.Add(new City { Cod = 724, Name = "PÁEZ" });
                //    state.Cities.Add(new City { Cod = 732, Name = "QUIPAMA" });
                //    state.Cities.Add(new City { Cod = 735, Name = "RAMIRIQUÍ" });
                //    state.Cities.Add(new City { Cod = 755, Name = "RONDÓN" });
                //    state.Cities.Add(new City { Cod = 758, Name = "RÁQUIRA" });
                //    state.Cities.Add(new City { Cod = 773, Name = "SABOYÁ" });
                //    state.Cities.Add(new City { Cod = 782, Name = "SAMACÁ" });
                //    state.Cities.Add(new City { Cod = 807, Name = "SAN EDUARDO" });
                //    state.Cities.Add(new City { Cod = 821, Name = "SAN JOSÉ DE PARE" });
                //    state.Cities.Add(new City { Cod = 837, Name = "SAN LUÍS DE GACENO" });
                //    state.Cities.Add(new City { Cod = 843, Name = "SAN MATEO" });
                //    state.Cities.Add(new City { Cod = 846, Name = "SAN MIGUEL DE SEMA" });
                //    state.Cities.Add(new City { Cod = 850, Name = "SAN PABLO DE BORBUR" });
                //    state.Cities.Add(new City { Cod = 878, Name = "SANTA MARÍA" });
                //    state.Cities.Add(new City { Cod = 884, Name = "SANTA ROSA DE VITERBO" });
                //    state.Cities.Add(new City { Cod = 887, Name = "SANTA SOFÍA" });
                //    state.Cities.Add(new City { Cod = 888, Name = "SANTANA" });
                //    state.Cities.Add(new City { Cod = 900, Name = "SATIVANORTE" });
                //    state.Cities.Add(new City { Cod = 901, Name = "SATIVASUR" });
                //    state.Cities.Add(new City { Cod = 905, Name = "SIACHOQUE" });
                //    state.Cities.Add(new City { Cod = 919, Name = "SOATÁ" });
                //    state.Cities.Add(new City { Cod = 920, Name = "SOCHA" });
                //    state.Cities.Add(new City { Cod = 922, Name = "SOCOTÁ" });
                //    state.Cities.Add(new City { Cod = 923, Name = "SOGAMOSO" });
                //    state.Cities.Add(new City { Cod = 927, Name = "SOMONDOCO" });
                //    state.Cities.Add(new City { Cod = 932, Name = "SORA" });
                //    state.Cities.Add(new City { Cod = 933, Name = "SORACÁ" });
                //    state.Cities.Add(new City { Cod = 934, Name = "SOTAQUIRÁ" });
                //    state.Cities.Add(new City { Cod = 949, Name = "SUSACÓN" });
                //    state.Cities.Add(new City { Cod = 950, Name = "SUTAMARCHÁN" });
                //    state.Cities.Add(new City { Cod = 952, Name = "SUTATENZA" });
                //    state.Cities.Add(new City { Cod = 956, Name = "SÁCHICA" });
                //    state.Cities.Add(new City { Cod = 968, Name = "TASCO" });
                //    state.Cities.Add(new City { Cod = 975, Name = "TENZA" });
                //    state.Cities.Add(new City { Cod = 980, Name = "TIBANÁ" });
                //    state.Cities.Add(new City { Cod = 981, Name = "TIBASOSA" });
                //    state.Cities.Add(new City { Cod = 988, Name = "TINJACÁ" });
                //    state.Cities.Add(new City { Cod = 989, Name = "TIPACOQUE" });
                //    state.Cities.Add(new City { Cod = 992, Name = "TOCA" });
                //    state.Cities.Add(new City { Cod = 995, Name = "TOGUÍ" });
                //    state.Cities.Add(new City { Cod = 1001, Name = "TOPAGÁ" });
                //    state.Cities.Add(new City { Cod = 1005, Name = "TOTA" });
                //    state.Cities.Add(new City { Cod = 1013, Name = "TUNJA" });
                //    state.Cities.Add(new City { Cod = 1014, Name = "TUNUNGUA" });
                //    state.Cities.Add(new City { Cod = 1018, Name = "TURMEQUÉ" });
                //    state.Cities.Add(new City { Cod = 1019, Name = "TUTA" });
                //    state.Cities.Add(new City { Cod = 1020, Name = "TUTASÁ" });
                //    state.Cities.Add(new City { Cod = 1049, Name = "VENTAQUEMADA" });
                //    state.Cities.Add(new City { Cod = 1058, Name = "VILLA DE LEIVA" });
                //    state.Cities.Add(new City { Cod = 1074, Name = "VIRACACHÁ" });
                //    state.Cities.Add(new City { Cod = 1093, Name = "ZETAQUIRÁ" });
                //    state.Cities.Add(new City { Cod = 1099, Name = "ÚMBITA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 17, Name = "CALDAS", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 10, Name = "AGUADAS" });
                //    state.Cities.Add(new City { Cod = 41, Name = "ANSERMA" });
                //    state.Cities.Add(new City { Cod = 50, Name = "ARANZAZU" });
                //    state.Cities.Add(new City { Cod = 90, Name = "BELALCÁZAR" });
                //    state.Cities.Add(new City { Cod = 195, Name = "CHINCHINÁ" });
                //    state.Cities.Add(new City { Cod = 334, Name = "FILADELFIA" });
                //    state.Cities.Add(new City { Cod = 461, Name = "LA DORADA" });
                //    state.Cities.Add(new City { Cod = 470, Name = "LA MERCED" });
                //    state.Cities.Add(new City { Cod = 493, Name = "LA VICTORIA" });
                //    state.Cities.Add(new City { Cod = 532, Name = "MANIZALES" });
                //    state.Cities.Add(new City { Cod = 534, Name = "MANZANARES" });
                //    state.Cities.Add(new City { Cod = 541, Name = "MARMATO" });
                //    state.Cities.Add(new City { Cod = 542, Name = "MARQUETALIA" });
                //    state.Cities.Add(new City { Cod = 544, Name = "MARULANDA" });
                //    state.Cities.Add(new City { Cod = 595, Name = "NEIRA" });
                //    state.Cities.Add(new City { Cod = 602, Name = "NORCASIA" });
                //    state.Cities.Add(new City { Cod = 634, Name = "PALESTINA" });
                //    state.Cities.Add(new City { Cod = 655, Name = "PENSILVANIA" });
                //    state.Cities.Add(new City { Cod = 723, Name = "PÁCORA" });
                //    state.Cities.Add(new City { Cod = 750, Name = "RISARALDA" });
                //    state.Cities.Add(new City { Cod = 761, Name = "RÍO SUCIO" });
                //    state.Cities.Add(new City { Cod = 776, Name = "SALAMINA" });
                //    state.Cities.Add(new City { Cod = 784, Name = "SAMANÁ" });
                //    state.Cities.Add(new City { Cod = 818, Name = "SAN JOSÉ" });
                //    state.Cities.Add(new City { Cod = 946, Name = "SUPÍA" });
                //    state.Cities.Add(new City { Cod = 1063, Name = "VILLAMARÍA" });
                //    state.Cities.Add(new City { Cod = 1076, Name = "VITERBO" });
                //    country.States.Add(state);
                //    state = new State { Cod = 18, Name = "CAQUETÁ", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 14, Name = "ALBANIA" });
                //    state.Cities.Add(new City { Cod = 98, Name = "BELÉN DE LOS ANDAQUÍES" });
                //    state.Cities.Add(new City { Cod = 172, Name = "CARTAGENA DEL CHAIRÁ" });
                //    state.Cities.Add(new City { Cod = 264, Name = "CURILLO" });
                //    state.Cities.Add(new City { Cod = 300, Name = "EL DONCELLO" });
                //    state.Cities.Add(new City { Cod = 308, Name = "EL PAUJIL" });
                //    state.Cities.Add(new City { Cod = 338, Name = "FLORENCIA" });
                //    state.Cities.Add(new City { Cod = 472, Name = "LA MONTAÑITA" });
                //    state.Cities.Add(new City { Cod = 555, Name = "MILÁN" });
                //    state.Cities.Add(new City { Cod = 577, Name = "MORELIA" });
                //    state.Cities.Add(new City { Cod = 710, Name = "PUERTO RICO" });
                //    state.Cities.Add(new City { Cod = 823, Name = "SAN JOSÉ DEL FRAGUA" });
                //    state.Cities.Add(new City { Cod = 862, Name = "SAN VICENTE DEL CAGUÁN" });
                //    state.Cities.Add(new City { Cod = 924, Name = "SOLANO" });
                //    state.Cities.Add(new City { Cod = 926, Name = "SOLITA" });
                //    state.Cities.Add(new City { Cod = 1044, Name = "VALPARAISO" });
                //    country.States.Add(state);
                //    state = new State { Cod = 19, Name = "CAUCA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 23, Name = "ALMAGUER" });
                //    state.Cities.Add(new City { Cod = 61, Name = "ARGELIA" });
                //    state.Cities.Add(new City { Cod = 76, Name = "BALBOA" });
                //    state.Cities.Add(new City { Cod = 111, Name = "BOLÍVAR" });
                //    state.Cities.Add(new City { Cod = 125, Name = "BUENOS AIRES" });
                //    state.Cities.Add(new City { Cod = 139, Name = "CAJIBÍO" });
                //    state.Cities.Add(new City { Cod = 146, Name = "CALDONO" });
                //    state.Cities.Add(new City { Cod = 149, Name = "CALOTO" });
                //    state.Cities.Add(new City { Cod = 242, Name = "CORINTO" });
                //    state.Cities.Add(new City { Cod = 321, Name = "EL TAMBO" });
                //    state.Cities.Add(new City { Cod = 339, Name = "FLORENCIA" });
                //    state.Cities.Add(new City { Cod = 384, Name = "GUACHENÉ" });
                //    state.Cities.Add(new City { Cod = 397, Name = "GUAPÍ" });
                //    state.Cities.Add(new City { Cod = 432, Name = "INZÁ" });
                //    state.Cities.Add(new City { Cod = 440, Name = "JAMBALÓ" });
                //    state.Cities.Add(new City { Cod = 482, Name = "LA SIERRA" });
                //    state.Cities.Add(new City { Cod = 490, Name = "LA VEGA" });
                //    state.Cities.Add(new City { Cod = 516, Name = "LÓPEZ (MICAY)" });
                //    state.Cities.Add(new City { Cod = 553, Name = "MERCADERES" });
                //    state.Cities.Add(new City { Cod = 558, Name = "MIRANDA" });
                //    state.Cities.Add(new City { Cod = 576, Name = "MORALES" });
                //    state.Cities.Add(new City { Cod = 627, Name = "PADILLA" });
                //    state.Cities.Add(new City { Cod = 648, Name = "PATÍA (EL BORDO)" });
                //    state.Cities.Add(new City { Cod = 660, Name = "PIAMONTE" });
                //    state.Cities.Add(new City { Cod = 663, Name = "PIENDAMÓ" });
                //    state.Cities.Add(new City { Cod = 679, Name = "POPAYÁN" });
                //    state.Cities.Add(new City { Cod = 715, Name = "PUERTO TEJADA" });
                //    state.Cities.Add(new City { Cod = 720, Name = "PURACÉ (COCONUCO)" });
                //    state.Cities.Add(new City { Cod = 725, Name = "PÁEZ (BELALCAZAR)" });
                //    state.Cities.Add(new City { Cod = 756, Name = "ROSAS" });
                //    state.Cities.Add(new City { Cod = 859, Name = "SAN SEBASTIÁN" });
                //    state.Cities.Add(new City { Cod = 881, Name = "SANTA ROSA" });
                //    state.Cities.Add(new City { Cod = 889, Name = "SANTANDER DE QUILICHAO" });
                //    state.Cities.Add(new City { Cod = 910, Name = "SILVIA" });
                //    state.Cities.Add(new City { Cod = 935, Name = "SOTARA (PAISPAMBA)" });
                //    state.Cities.Add(new City { Cod = 941, Name = "SUCRE" });
                //    state.Cities.Add(new City { Cod = 953, Name = "SUÁREZ" });
                //    state.Cities.Add(new City { Cod = 986, Name = "TIMBIQUÍ" });
                //    state.Cities.Add(new City { Cod = 987, Name = "TIMBÍO" });
                //    state.Cities.Add(new City { Cod = 1003, Name = "TORIBÍO" });
                //    state.Cities.Add(new City { Cod = 1006, Name = "TOTORÓ" });
                //    state.Cities.Add(new City { Cod = 1057, Name = "VILLA RICA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 20, Name = "CESAR", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 8, Name = "AGUACHICA" });
                //    state.Cities.Add(new City { Cod = 12, Name = "AGUSTÍN CODAZZI" });
                //    state.Cities.Add(new City { Cod = 69, Name = "ASTREA" });
                //    state.Cities.Add(new City { Cod = 89, Name = "BECERRÍL" });
                //    state.Cities.Add(new City { Cod = 114, Name = "BOSCONIA" });
                //    state.Cities.Add(new City { Cod = 192, Name = "CHIMICHAGUA" });
                //    state.Cities.Add(new City { Cod = 201, Name = "CHIRIGUANÁ" });
                //    state.Cities.Add(new City { Cod = 266, Name = "CURUMANÍ" });
                //    state.Cities.Add(new City { Cod = 299, Name = "EL COPEY" });
                //    state.Cities.Add(new City { Cod = 307, Name = "EL PASO" });
                //    state.Cities.Add(new City { Cod = 366, Name = "GAMARRA" });
                //    state.Cities.Add(new City { Cod = 375, Name = "GONZALEZ" });
                //    state.Cities.Add(new City { Cod = 465, Name = "LA GLORIA" });
                //    state.Cities.Add(new City { Cod = 466, Name = "LA JAGUA DE IBIRICO" });
                //    state.Cities.Add(new City { Cod = 475, Name = "LA PAZ (ROBLES)" });
                //    state.Cities.Add(new City { Cod = 531, Name = "MANAURE BALCÓN DEL CESAR" });
                //    state.Cities.Add(new City { Cod = 629, Name = "PAILITAS" });
                //    state.Cities.Add(new City { Cod = 654, Name = "PELAYA" });
                //    state.Cities.Add(new City { Cod = 686, Name = "PUEBLO BELLO" });
                //    state.Cities.Add(new City { Cod = 763, Name = "RÍO DE ORO" });
                //    state.Cities.Add(new City { Cod = 787, Name = "SAN ALBERTO" });
                //    state.Cities.Add(new City { Cod = 806, Name = "SAN DIEGO" });
                //    state.Cities.Add(new City { Cod = 840, Name = "SAN MARTÍN" });
                //    state.Cities.Add(new City { Cod = 960, Name = "TAMALAMEQUE" });
                //    state.Cities.Add(new City { Cod = 1042, Name = "VALLEDUPAR" });
                //    country.States.Add(state);
                //    state = new State { Cod = 23, Name = "CÓRDOBA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 72, Name = "AYAPEL" });
                //    state.Cities.Add(new City { Cod = 122, Name = "BUENAVISTA" });
                //    state.Cities.Add(new City { Cod = 155, Name = "CANALETE" });
                //    state.Cities.Add(new City { Cod = 180, Name = "CERETÉ" });
                //    state.Cities.Add(new City { Cod = 193, Name = "CHIMÁ" });
                //    state.Cities.Add(new City { Cod = 197, Name = "CHINÚ" });
                //    state.Cities.Add(new City { Cod = 220, Name = "CIÉNAGA DE ORO" });
                //    state.Cities.Add(new City { Cod = 247, Name = "COTORRA" });
                //    state.Cities.Add(new City { Cod = 452, Name = "LA APARTADA Y LA FRONTERA" });
                //    state.Cities.Add(new City { Cod = 507, Name = "LORICA" });
                //    state.Cities.Add(new City { Cod = 508, Name = "LOS CÓRDOBAS" });
                //    state.Cities.Add(new City { Cod = 564, Name = "MOMIL" });
                //    state.Cities.Add(new City { Cod = 571, Name = "MONTELÍBANO" });
                //    state.Cities.Add(new City { Cod = 573, Name = "MONTERIA" });
                //    state.Cities.Add(new City { Cod = 582, Name = "MOÑITOS" });
                //    state.Cities.Add(new City { Cod = 674, Name = "PLANETA RICA" });
                //    state.Cities.Add(new City { Cod = 687, Name = "PUEBLO NUEVO" });
                //    state.Cities.Add(new City { Cod = 700, Name = "PUERTO ESCONDIDO" });
                //    state.Cities.Add(new City { Cod = 704, Name = "PUERTO LIBERTADOR" });
                //    state.Cities.Add(new City { Cod = 722, Name = "PURÍSIMA" });
                //    state.Cities.Add(new City { Cod = 774, Name = "SAHAGÚN" });
                //    state.Cities.Add(new City { Cod = 789, Name = "SAN ANDRÉS SOTAVENTO" });
                //    state.Cities.Add(new City { Cod = 791, Name = "SAN ANTERO" });
                //    state.Cities.Add(new City { Cod = 798, Name = "SAN BERNARDO DEL VIENTO" });
                //    state.Cities.Add(new City { Cod = 801, Name = "SAN CARLOS" });
                //    state.Cities.Add(new City { Cod = 822, Name = "SAN JOSÉ DE URÉ" });
                //    state.Cities.Add(new City { Cod = 856, Name = "SAN PELAYO" });
                //    state.Cities.Add(new City { Cod = 984, Name = "TIERRALTA" });
                //    state.Cities.Add(new City { Cod = 1010, Name = "TUCHÍN" });
                //    state.Cities.Add(new City { Cod = 1038, Name = "VALENCIA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 25, Name = "CUNDINAMARCA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 7, Name = "AGUA DE DIOS" });
                //    state.Cities.Add(new City { Cod = 17, Name = "ALBÁN" });
                //    state.Cities.Add(new City { Cod = 33, Name = "ANAPOIMA" });
                //    state.Cities.Add(new City { Cod = 39, Name = "ANOLAIMA" });
                //    state.Cities.Add(new City { Cod = 46, Name = "APULO" });
                //    state.Cities.Add(new City { Cod = 54, Name = "ARBELÁEZ" });
                //    state.Cities.Add(new City { Cod = 93, Name = "BELTRÁN" });
                //    state.Cities.Add(new City { Cod = 104, Name = "BITUIMA" });
                //    state.Cities.Add(new City { Cod = 108, Name = "BOJACÁ" });
                //    state.Cities.Add(new City { Cod = 131, Name = "CABRERA" });
                //    state.Cities.Add(new City { Cod = 134, Name = "CACHIPAY" });
                //    state.Cities.Add(new City { Cod = 140, Name = "CAJICÁ" });
                //    state.Cities.Add(new City { Cod = 160, Name = "CAPARRAPÍ" });
                //    state.Cities.Add(new City { Cod = 167, Name = "CARMEN DE CARUPA" });
                //    state.Cities.Add(new City { Cod = 185, Name = "CHAGUANÍ" });
                //    state.Cities.Add(new City { Cod = 198, Name = "CHIPAQUE" });
                //    state.Cities.Add(new City { Cod = 208, Name = "CHOACHÍ" });
                //    state.Cities.Add(new City { Cod = 209, Name = "CHOCONTÁ" });
                //    state.Cities.Add(new City { Cod = 211, Name = "CHÍA" });
                //    state.Cities.Add(new City { Cod = 224, Name = "COGUA" });
                //    state.Cities.Add(new City { Cod = 246, Name = "COTA" });
                //    state.Cities.Add(new City { Cod = 256, Name = "CUCUNUBÁ" });
                //    state.Cities.Add(new City { Cod = 270, Name = "CÁQUEZA" });
                //    state.Cities.Add(new City { Cod = 298, Name = "EL COLEGIO" });
                //    state.Cities.Add(new City { Cod = 312, Name = "EL PEÑÓN" });
                //    state.Cities.Add(new City { Cod = 318, Name = "EL ROSAL" });
                //    state.Cities.Add(new City { Cod = 332, Name = "FACATATIVÁ" });
                //    state.Cities.Add(new City { Cod = 346, Name = "FOSCA" });
                //    state.Cities.Add(new City { Cod = 354, Name = "FUNZA" });
                //    state.Cities.Add(new City { Cod = 355, Name = "FUSAGASUGÁ" });
                //    state.Cities.Add(new City { Cod = 356, Name = "FÓMEQUE" });
                //    state.Cities.Add(new City { Cod = 357, Name = "FÚQUENE" });
                //    state.Cities.Add(new City { Cod = 358, Name = "GACHALÁ" });
                //    state.Cities.Add(new City { Cod = 359, Name = "GACHANCIPÁ" });
                //    state.Cities.Add(new City { Cod = 361, Name = "GACHETÁ" });
                //    state.Cities.Add(new City { Cod = 365, Name = "GAMA" });
                //    state.Cities.Add(new City { Cod = 372, Name = "GIRARDOT" });
                //    state.Cities.Add(new City { Cod = 378, Name = "GRANADA" });
                //    state.Cities.Add(new City { Cod = 385, Name = "GUACHETÁ" });
                //    state.Cities.Add(new City { Cod = 390, Name = "GUADUAS" });
                //    state.Cities.Add(new City { Cod = 400, Name = "GUASCA" });
                //    state.Cities.Add(new City { Cod = 402, Name = "GUATAQUÍ" });
                //    state.Cities.Add(new City { Cod = 403, Name = "GUATAVITA" });
                //    state.Cities.Add(new City { Cod = 406, Name = "GUAYABAL DE SIQUIMA" });
                //    state.Cities.Add(new City { Cod = 407, Name = "GUAYABETAL" });
                //    state.Cities.Add(new City { Cod = 411, Name = "GUTIÉRREZ" });
                //    state.Cities.Add(new City { Cod = 446, Name = "JERUSALÉN" });
                //    state.Cities.Add(new City { Cod = 450, Name = "JUNÍN" });
                //    state.Cities.Add(new City { Cod = 455, Name = "LA CALERA" });
                //    state.Cities.Add(new City { Cod = 471, Name = "LA MESA" });
                //    state.Cities.Add(new City { Cod = 473, Name = "LA PALMA" });
                //    state.Cities.Add(new City { Cod = 476, Name = "LA PEÑA" });
                //    state.Cities.Add(new City { Cod = 491, Name = "LA VEGA" });
                //    state.Cities.Add(new City { Cod = 502, Name = "LENGUAZAQUE" });
                //    state.Cities.Add(new City { Cod = 520, Name = "MACHETÁ" });
                //    state.Cities.Add(new City { Cod = 521, Name = "MADRID" });
                //    state.Cities.Add(new City { Cod = 533, Name = "MANTA" });
                //    state.Cities.Add(new City { Cod = 548, Name = "MEDINA" });
                //    state.Cities.Add(new City { Cod = 579, Name = "MOSQUERA" });
                //    state.Cities.Add(new City { Cod = 590, Name = "NARIÑO" });
                //    state.Cities.Add(new City { Cod = 597, Name = "NEMOCÓN" });
                //    state.Cities.Add(new City { Cod = 598, Name = "NILO" });
                //    state.Cities.Add(new City { Cod = 599, Name = "NIMAIMA" });
                //    state.Cities.Add(new City { Cod = 601, Name = "NOCAIMA" });
                //    state.Cities.Add(new City { Cod = 626, Name = "PACHO" });
                //    state.Cities.Add(new City { Cod = 630, Name = "PAIME" });
                //    state.Cities.Add(new City { Cod = 644, Name = "PANDI" });
                //    state.Cities.Add(new City { Cod = 646, Name = "PARATEBUENO" });
                //    state.Cities.Add(new City { Cod = 647, Name = "PASCA" });
                //    state.Cities.Add(new City { Cod = 713, Name = "PUERTO SALGAR" });
                //    state.Cities.Add(new City { Cod = 718, Name = "PULÍ" });
                //    state.Cities.Add(new City { Cod = 727, Name = "QUEBRACodGRA" });
                //    state.Cities.Add(new City { Cod = 728, Name = "QUETAME" });
                //    state.Cities.Add(new City { Cod = 733, Name = "QUIPILE" });
                //    state.Cities.Add(new City { Cod = 744, Name = "RICAURTE" });
                //    state.Cities.Add(new City { Cod = 793, Name = "SAN ANTONIO DE TEQUENDAMA" });
                //    state.Cities.Add(new City { Cod = 796, Name = "SAN BERNARDO" });
                //    state.Cities.Add(new City { Cod = 803, Name = "SAN CAYETANO" });
                //    state.Cities.Add(new City { Cod = 811, Name = "SAN FRANCISCO" });
                //    state.Cities.Add(new City { Cod = 830, Name = "SAN JUAN DE RÍO SECO" });
                //    state.Cities.Add(new City { Cod = 899, Name = "SASAIMA" });
                //    state.Cities.Add(new City { Cod = 903, Name = "SESQUILÉ" });
                //    state.Cities.Add(new City { Cod = 906, Name = "SIBATÉ" });
                //    state.Cities.Add(new City { Cod = 909, Name = "SILVANIA" });
                //    state.Cities.Add(new City { Cod = 912, Name = "SIMIJACA" });
                //    state.Cities.Add(new City { Cod = 918, Name = "SOACHA" });
                //    state.Cities.Add(new City { Cod = 931, Name = "SOPÓ" });
                //    state.Cities.Add(new City { Cod = 940, Name = "SUBACHOQUE" });
                //    state.Cities.Add(new City { Cod = 944, Name = "SUESCA" });
                //    state.Cities.Add(new City { Cod = 945, Name = "SUPATÁ" });
                //    state.Cities.Add(new City { Cod = 948, Name = "SUSA" });
                //    state.Cities.Add(new City { Cod = 951, Name = "SUTATAUSA" });
                //    state.Cities.Add(new City { Cod = 957, Name = "TABIO" });
                //    state.Cities.Add(new City { Cod = 970, Name = "TAUSA" });
                //    state.Cities.Add(new City { Cod = 972, Name = "TENA" });
                //    state.Cities.Add(new City { Cod = 974, Name = "TENJO" });
                //    state.Cities.Add(new City { Cod = 979, Name = "TIBACUY" });
                //    state.Cities.Add(new City { Cod = 982, Name = "TIBIRITA" });
                //    state.Cities.Add(new City { Cod = 993, Name = "TOCAIMA" });
                //    state.Cities.Add(new City { Cod = 994, Name = "TOCANCIPÁ" });
                //    state.Cities.Add(new City { Cod = 1002, Name = "TOPAIPÍ" });
                //    state.Cities.Add(new City { Cod = 1024, Name = "UBALÁ" });
                //    state.Cities.Add(new City { Cod = 1025, Name = "UBAQUE" });
                //    state.Cities.Add(new City { Cod = 1026, Name = "UBATÉ" });
                //    state.Cities.Add(new City { Cod = 1028, Name = "UNE" });
                //    state.Cities.Add(new City { Cod = 1048, Name = "VENECIA (OSPINA PÉREZ)" });
                //    state.Cities.Add(new City { Cod = 1050, Name = "VERGARA" });
                //    state.Cities.Add(new City { Cod = 1053, Name = "VIANI" });
                //    state.Cities.Add(new City { Cod = 1061, Name = "VILLAGÓMEZ" });
                //    state.Cities.Add(new City { Cod = 1068, Name = "VILLAPINZÓN" });
                //    state.Cities.Add(new City { Cod = 1072, Name = "VILLETA" });
                //    state.Cities.Add(new City { Cod = 1073, Name = "VIOTÁ" });
                //    state.Cities.Add(new City { Cod = 1078, Name = "YACOPÍ" });
                //    state.Cities.Add(new City { Cod = 1094, Name = "ZIPACÓN" });
                //    state.Cities.Add(new City { Cod = 1095, Name = "ZIPAQUIRÁ" });
                //    state.Cities.Add(new City { Cod = 1100, Name = "ÚTICA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 27, Name = "CHOCÓ", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 3, Name = "ACANDÍ" });
                //    state.Cities.Add(new City { Cod = 27, Name = "ALTO BAUDÓ (PIE DE PATO)" });
                //    state.Cities.Add(new City { Cod = 71, Name = "ATRATO (YUTO)" });
                //    state.Cities.Add(new City { Cod = 73, Name = "BAGADÓ" });
                //    state.Cities.Add(new City { Cod = 74, Name = "BAHÍA SOLANO (MÚTIS)" });
                //    state.Cities.Add(new City { Cod = 75, Name = "BAJO BAUDÓ (PIZARRO)" });
                //    state.Cities.Add(new City { Cod = 96, Name = "BELÉN DE BAJIRÁ" });
                //    state.Cities.Add(new City { Cod = 109, Name = "BOJAYÁ (BELLAVISTA)" });
                //    state.Cities.Add(new City { Cod = 159, Name = "CANTÓN DE SAN PABLO" });
                //    state.Cities.Add(new City { Cod = 169, Name = "CARMEN DEL DARIÉN (CURBARADÓ)" });
                //    state.Cities.Add(new City { Cod = 233, Name = "CONDOTO" });
                //    state.Cities.Add(new City { Cod = 271, Name = "CÉRTEGUI" });
                //    state.Cities.Add(new City { Cod = 292, Name = "EL CARMEN DE ATRATO" });
                //    state.Cities.Add(new City { Cod = 436, Name = "ISTMINA" });
                //    state.Cities.Add(new City { Cod = 451, Name = "JURADÓ" });
                //    state.Cities.Add(new City { Cod = 506, Name = "LLORÓ" });
                //    state.Cities.Add(new City { Cod = 549, Name = "MEDIO ATRATO" });
                //    state.Cities.Add(new City { Cod = 550, Name = "MEDIO BAUDÓ" });
                //    state.Cities.Add(new City { Cod = 551, Name = "MEDIO SAN JUAN (ANDAGOYA)" });
                //    state.Cities.Add(new City { Cod = 604, Name = "NOVITA" });
                //    state.Cities.Add(new City { Cod = 608, Name = "NUQUÍ" });
                //    state.Cities.Add(new City { Cod = 729, Name = "QUIBDÓ" });
                //    state.Cities.Add(new City { Cod = 759, Name = "RÍO IRÓ" });
                //    state.Cities.Add(new City { Cod = 760, Name = "RÍO QUITO" });
                //    state.Cities.Add(new City { Cod = 765, Name = "RÍOSUCIO" });
                //    state.Cities.Add(new City { Cod = 825, Name = "SAN JOSÉ DEL PALMAR" });
                //    state.Cities.Add(new City { Cod = 873, Name = "SANTA GENOVEVA DE DOCORODÓ" });
                //    state.Cities.Add(new City { Cod = 916, Name = "SIPÍ" });
                //    state.Cities.Add(new City { Cod = 958, Name = "TADÓ" });
                //    state.Cities.Add(new City { Cod = 1029, Name = "UNGUÍA" });
                //    state.Cities.Add(new City { Cod = 1030, Name = "UNIÓN PANAMERICANA (ÁNIMAS)" });
                //    country.States.Add(state);
                //    state = new State { Cod = 41, Name = "HUILA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 4, Name = "ACEVEDO" });
                //    state.Cities.Add(new City { Cod = 6, Name = "AGRADO" });
                //    state.Cities.Add(new City { Cod = 13, Name = "AIPE" });
                //    state.Cities.Add(new City { Cod = 22, Name = "ALGECIRAS" });
                //    state.Cities.Add(new City { Cod = 26, Name = "ALTAMIRA" });
                //    state.Cities.Add(new City { Cod = 79, Name = "BARAYA" });
                //    state.Cities.Add(new City { Cod = 153, Name = "CAMPOALEGRE" });
                //    state.Cities.Add(new City { Cod = 225, Name = "COLOMBIA" });
                //    state.Cities.Add(new City { Cod = 326, Name = "ELÍAS" });
                //    state.Cities.Add(new City { Cod = 368, Name = "GARZÓN" });
                //    state.Cities.Add(new City { Cod = 369, Name = "GIGANTE" });
                //    state.Cities.Add(new City { Cod = 388, Name = "GUADALUPE" });
                //    state.Cities.Add(new City { Cod = 426, Name = "HOBO" });
                //    state.Cities.Add(new City { Cod = 435, Name = "ISNOS" });
                //    state.Cities.Add(new City { Cod = 453, Name = "LA ARGENTINA" });
                //    state.Cities.Add(new City { Cod = 478, Name = "LA PLATA" });
                //    state.Cities.Add(new City { Cod = 596, Name = "NEIVA" });
                //    state.Cities.Add(new City { Cod = 609, Name = "NÁTAGA" });
                //    state.Cities.Add(new City { Cod = 618, Name = "OPORAPA" });
                //    state.Cities.Add(new City { Cod = 628, Name = "PAICOL" });
                //    state.Cities.Add(new City { Cod = 633, Name = "PALERMO" });
                //    state.Cities.Add(new City { Cod = 635, Name = "PALESTINA" });
                //    state.Cities.Add(new City { Cod = 670, Name = "PITAL" });
                //    state.Cities.Add(new City { Cod = 671, Name = "PITALITO" });
                //    state.Cities.Add(new City { Cod = 751, Name = "RIVERA" });
                //    state.Cities.Add(new City { Cod = 775, Name = "SALADOBLANCO" });
                //    state.Cities.Add(new City { Cod = 786, Name = "SAN AGUSTÍN" });
                //    state.Cities.Add(new City { Cod = 879, Name = "SANTA MARÍA" });
                //    state.Cities.Add(new City { Cod = 939, Name = "SUAZA" });
                //    state.Cities.Add(new City { Cod = 966, Name = "TARQUI" });
                //    state.Cities.Add(new City { Cod = 971, Name = "TELLO" });
                //    state.Cities.Add(new City { Cod = 977, Name = "TERUEL" });
                //    state.Cities.Add(new City { Cod = 978, Name = "TESALIA" });
                //    state.Cities.Add(new City { Cod = 985, Name = "TIMANÁ" });
                //    state.Cities.Add(new City { Cod = 1071, Name = "VILLAVIEJA" });
                //    state.Cities.Add(new City { Cod = 1080, Name = "YAGUARÁ" });
                //    state.Cities.Add(new City { Cod = 1098, Name = "ÍQUIRA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 44, Name = "LA GUAJIRA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 15, Name = "ALBANIA" });
                //    state.Cities.Add(new City { Cod = 86, Name = "BARRANCAS" });
                //    state.Cities.Add(new City { Cod = 278, Name = "DIBULLA" });
                //    state.Cities.Add(new City { Cod = 279, Name = "DISTRACCIÓN" });
                //    state.Cities.Add(new City { Cod = 306, Name = "EL MOLINO" });
                //    state.Cities.Add(new City { Cod = 344, Name = "FONSECA" });
                //    state.Cities.Add(new City { Cod = 421, Name = "HATONUEVO" });
                //    state.Cities.Add(new City { Cod = 467, Name = "LA JAGUA DEL PILAR" });
                //    state.Cities.Add(new City { Cod = 525, Name = "MAICAO" });
                //    state.Cities.Add(new City { Cod = 530, Name = "MANAURE" });
                //    state.Cities.Add(new City { Cod = 749, Name = "RIOHACHA" });
                //    state.Cities.Add(new City { Cod = 832, Name = "SAN JUAN DEL CESAR" });
                //    state.Cities.Add(new City { Cod = 1033, Name = "URIBIA" });
                //    state.Cities.Add(new City { Cod = 1035, Name = "URUMITA" });
                //    state.Cities.Add(new City { Cod = 1065, Name = "VILLANUEVA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 47, Name = "MAGDALENA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 21, Name = "ALGARROBO" });
                //    state.Cities.Add(new City { Cod = 49, Name = "ARACATACA" });
                //    state.Cities.Add(new City { Cod = 63, Name = "ARIGUANÍ (EL DIFÍCIL)" });
                //    state.Cities.Add(new City { Cod = 183, Name = "CERRO SAN ANTONIO" });
                //    state.Cities.Add(new City { Cod = 207, Name = "CHIVOLO" });
                //    state.Cities.Add(new City { Cod = 219, Name = "CIÉNAGA" });
                //    state.Cities.Add(new City { Cod = 232, Name = "CONCORDIA" });
                //    state.Cities.Add(new City { Cod = 287, Name = "EL BANCO" });
                //    state.Cities.Add(new City { Cod = 313, Name = "EL PIÑON" });
                //    state.Cities.Add(new City { Cod = 316, Name = "EL RETÉN" });
                //    state.Cities.Add(new City { Cod = 352, Name = "FUNDACIÓN" });
                //    state.Cities.Add(new City { Cod = 393, Name = "GUAMAL" });
                //    state.Cities.Add(new City { Cod = 605, Name = "NUEVA GRANADA" });
                //    state.Cities.Add(new City { Cod = 653, Name = "PEDRAZA" });
                //    state.Cities.Add(new City { Cod = 665, Name = "PIJIÑO" });
                //    state.Cities.Add(new City { Cod = 672, Name = "PIVIJAY" });
                //    state.Cities.Add(new City { Cod = 675, Name = "PLATO" });
                //    state.Cities.Add(new City { Cod = 690, Name = "PUEBLOVIEJO" });
                //    state.Cities.Add(new City { Cod = 739, Name = "REMOLINO" });
                //    state.Cities.Add(new City { Cod = 771, Name = "SABANAS DE SAN ANGEL (SAN ANGEL)" });
                //    state.Cities.Add(new City { Cod = 777, Name = "SALAMINA" });
                //    state.Cities.Add(new City { Cod = 860, Name = "SAN SEBASTIÁN DE BUENAVISTA" });
                //    state.Cities.Add(new City { Cod = 864, Name = "SAN ZENÓN" });
                //    state.Cities.Add(new City { Cod = 866, Name = "SANTA ANA" });
                //    state.Cities.Add(new City { Cod = 870, Name = "SANTA BÁRBARA DE PINTO" });
                //    state.Cities.Add(new City { Cod = 877, Name = "SANTA MARTA" });
                //    state.Cities.Add(new City { Cod = 917, Name = "SITIONUEVO" });
                //    state.Cities.Add(new City { Cod = 973, Name = "TENERIFE" });
                //    state.Cities.Add(new City { Cod = 1090, Name = "ZAPAYÁN (PUNTA DE PIEDRAS)" });
                //    state.Cities.Add(new City { Cod = 1096, Name = "ZONA BANANERA (PRADO - SEVILLA)" });
                //    country.States.Add(state);
                //    state = new State { Cod = 50, Name = "META", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 2, Name = "ACACÍAS" });
                //    state.Cities.Add(new City { Cod = 84, Name = "BARRANCA DE UPÍA" });
                //    state.Cities.Add(new City { Cod = 133, Name = "CABUYARO" });
                //    state.Cities.Add(new City { Cod = 176, Name = "CASTILLA LA NUEVA" });
                //    state.Cities.Add(new City { Cod = 253, Name = "CUBARRAL" });
                //    state.Cities.Add(new City { Cod = 259, Name = "CUMARAL" });
                //    state.Cities.Add(new City { Cod = 289, Name = "EL CALVARIO" });
                //    state.Cities.Add(new City { Cod = 294, Name = "EL CASTILLO" });
                //    state.Cities.Add(new City { Cod = 301, Name = "EL DORADO" });
                //    state.Cities.Add(new City { Cod = 351, Name = "FUENTE DE ORO" });
                //    state.Cities.Add(new City { Cod = 379, Name = "GRANADA" });
                //    state.Cities.Add(new City { Cod = 394, Name = "GUAMAL" });
                //    state.Cities.Add(new City { Cod = 469, Name = "LA MACARENA" });
                //    state.Cities.Add(new City { Cod = 501, Name = "LEJANÍAS" });
                //    state.Cities.Add(new City { Cod = 536, Name = "MAPIRIPAN" });
                //    state.Cities.Add(new City { Cod = 554, Name = "MESETAS" });
                //    state.Cities.Add(new City { Cod = 699, Name = "PUERTO CONCORDIA" });
                //    state.Cities.Add(new City { Cod = 701, Name = "PUERTO GAITÁN" });
                //    state.Cities.Add(new City { Cod = 705, Name = "PUERTO LLERAS" });
                //    state.Cities.Add(new City { Cod = 706, Name = "PUERTO LÓPEZ" });
                //    state.Cities.Add(new City { Cod = 711, Name = "PUERTO RICO" });
                //    state.Cities.Add(new City { Cod = 741, Name = "RESTREPO" });
                //    state.Cities.Add(new City { Cod = 802, Name = "SAN CARLOS DE GUAROA" });
                //    state.Cities.Add(new City { Cod = 826, Name = "SAN JUAN DE ARAMA" });
                //    state.Cities.Add(new City { Cod = 833, Name = "SAN JUANITO" });
                //    state.Cities.Add(new City { Cod = 841, Name = "SAN MARTÍN" });
                //    state.Cities.Add(new City { Cod = 1032, Name = "URIBE" });
                //    state.Cities.Add(new City { Cod = 1070, Name = "VILLAVICENCIO" });
                //    state.Cities.Add(new City { Cod = 1075, Name = "VISTA HERMOSA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 52, Name = "NARIÑO", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 18, Name = "ALBÁN (SAN JOSÉ)" });
                //    state.Cities.Add(new City { Cod = 34, Name = "ANCUYA" });
                //    state.Cities.Add(new City { Cod = 55, Name = "ARBOLEDA (BERRUECOS)" });
                //    state.Cities.Add(new City { Cod = 80, Name = "BARBACOAS" });
                //    state.Cities.Add(new City { Cod = 95, Name = "BELÉN" });
                //    state.Cities.Add(new City { Cod = 126, Name = "BUESACO" });
                //    state.Cities.Add(new City { Cod = 184, Name = "CHACHAGUÍ" });
                //    state.Cities.Add(new City { Cod = 228, Name = "COLÓN (GÉNOVA)" });
                //    state.Cities.Add(new City { Cod = 235, Name = "CONSACA" });
                //    state.Cities.Add(new City { Cod = 236, Name = "CONTADERO" });
                //    state.Cities.Add(new City { Cod = 252, Name = "CUASPUD (CARLOSAMA)" });
                //    state.Cities.Add(new City { Cod = 261, Name = "CUMBAL" });
                //    state.Cities.Add(new City { Cod = 262, Name = "CUMBITARA" });
                //    state.Cities.Add(new City { Cod = 274, Name = "CÓRDOBA" });
                //    state.Cities.Add(new City { Cod = 296, Name = "EL CHARCO" });
                //    state.Cities.Add(new City { Cod = 309, Name = "EL PEÑOL" });
                //    state.Cities.Add(new City { Cod = 319, Name = "EL ROSARIO" });
                //    state.Cities.Add(new City { Cod = 320, Name = "EL TABLÓN DE GÓMEZ" });
                //    state.Cities.Add(new City { Cod = 322, Name = "EL TAMBO" });
                //    state.Cities.Add(new City { Cod = 347, Name = "FRANCISCO PIZARRO" });
                //    state.Cities.Add(new City { Cod = 353, Name = "FUNES" });
                //    state.Cities.Add(new City { Cod = 383, Name = "GUACHAVÉS" });
                //    state.Cities.Add(new City { Cod = 386, Name = "GUACHUCAL" });
                //    state.Cities.Add(new City { Cod = 391, Name = "GUAITARILLA" });
                //    state.Cities.Add(new City { Cod = 392, Name = "GUALMATÁN" });
                //    state.Cities.Add(new City { Cod = 430, Name = "ILES" });
                //    state.Cities.Add(new City { Cod = 431, Name = "IMÚES" });
                //    state.Cities.Add(new City { Cod = 434, Name = "IPIALES" });
                //    state.Cities.Add(new City { Cod = 459, Name = "LA CRUZ" });
                //    state.Cities.Add(new City { Cod = 464, Name = "LA FLORIDA" });
                //    state.Cities.Add(new City { Cod = 468, Name = "LA LLANADA" });
                //    state.Cities.Add(new City { Cod = 484, Name = "LA TOLA" });
                //    state.Cities.Add(new City { Cod = 486, Name = "LA UNIÓN" });
                //    state.Cities.Add(new City { Cod = 500, Name = "LEIVA" });
                //    state.Cities.Add(new City { Name = "LINARES" });
                //    state.Cities.Add(new City { Cod = 523, Name = "MAGÜI (PAYÁN)" });
                //    state.Cities.Add(new City { Cod = 528, Name = "MALLAMA (PIEDRANCHA)" });
                //    state.Cities.Add(new City { Cod = 580, Name = "MOSQUERA" });
                //    state.Cities.Add(new City { Cod = 591, Name = "NARIÑO" });
                //    state.Cities.Add(new City { Cod = 616, Name = "OLAYA HERRERA" });
                //    state.Cities.Add(new City { Cod = 622, Name = "OSPINA" });
                //    state.Cities.Add(new City { Cod = 676, Name = "POLICARPA" });
                //    state.Cities.Add(new City { Cod = 681, Name = "POTOSÍ" });
                //    state.Cities.Add(new City { Cod = 684, Name = "PROVIDENCIA" });
                //    state.Cities.Add(new City { Cod = 692, Name = "PUERRES" });
                //    state.Cities.Add(new City { Cod = 719, Name = "PUPIALES" });
                //    state.Cities.Add(new City { Cod = 745, Name = "RICAURTE" });
                //    state.Cities.Add(new City { Cod = 752, Name = "ROBERTO PAYÁN (SAN JOSÉ)" });
                //    state.Cities.Add(new City { Cod = 783, Name = "SAMANIEGO" });
                //    state.Cities.Add(new City { Cod = 797, Name = "SAN BERNARDO" });
                //    state.Cities.Add(new City { Cod = 829, Name = "SAN JUAN DE PASTO" });
                //    state.Cities.Add(new City { Cod = 834, Name = "SAN LORENZO" });
                //    state.Cities.Add(new City { Cod = 849, Name = "SAN PABLO" });
                //    state.Cities.Add(new City { Cod = 854, Name = "SAN PEDRO DE CARTAGO" });
                //    state.Cities.Add(new City { Cod = 865, Name = "SANDONÁ" });
                //    state.Cities.Add(new City { Cod = 869, Name = "SANTA BÁRBARA (ISCUANDÉ)" });
                //    state.Cities.Add(new City { Cod = 896, Name = "SAPUYES" });
                //    state.Cities.Add(new City { Cod = 936, Name = "SOTOMAYOR (LOS ANDES)" });
                //    state.Cities.Add(new City { Cod = 962, Name = "TAMINANGO" });
                //    state.Cities.Add(new City { Cod = 963, Name = "TANGUA" });
                //    state.Cities.Add(new City { Cod = 1012, Name = "TUMACO" });
                //    state.Cities.Add(new City { Cod = 1023, Name = "TÚQUERRES" });
                //    state.Cities.Add(new City { Cod = 1079, Name = "YACUANQUER" });
                //    country.States.Add(state);
                //    state = new State { Cod = 54, Name = "NORTE DE SANTANDER", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 56, Name = "ARBOLEDAS" });
                //    state.Cities.Add(new City { Cod = 106, Name = "BOCHALEMA" });
                //    state.Cities.Add(new City { Cod = 119, Name = "BUCARASICA" });
                //    state.Cities.Add(new City { Cod = 196, Name = "CHINÁCOTA" });
                //    state.Cities.Add(new City { Cod = 204, Name = "CHITAGÁ" });
                //    state.Cities.Add(new City { Cod = 238, Name = "CONVENCIÓN" });
                //    state.Cities.Add(new City { Cod = 257, Name = "CUCUTILLA" });
                //    state.Cities.Add(new City { Cod = 268, Name = "CÁCHIRA" });
                //    state.Cities.Add(new City { Cod = 269, Name = "CÁCOTA" });
                //    state.Cities.Add(new City { Cod = 275, Name = "CÚCUTA" });
                //    state.Cities.Add(new City { Cod = 284, Name = "DURANIA" });
                //    state.Cities.Add(new City { Cod = 290, Name = "EL CARMEN" });
                //    state.Cities.Add(new City { Cod = 323, Name = "EL TARRA" });
                //    state.Cities.Add(new City { Cod = 324, Name = "EL ZULIA" });
                //    state.Cities.Add(new City { Cod = 376, Name = "GRAMALOTE" });
                //    state.Cities.Add(new City { Cod = 417, Name = "HACARÍ" });
                //    state.Cities.Add(new City { Cod = 423, Name = "HERRÁN" });
                //    state.Cities.Add(new City { Cod = 462, Name = "LA ESPERANZA" });
                //    state.Cities.Add(new City { Cod = 479, Name = "LA PLAYA" });
                //    state.Cities.Add(new City { Cod = 496, Name = "LABATECA" });
                //    state.Cities.Add(new City { Cod = 510, Name = "LOS PATIOS" });
                //    state.Cities.Add(new City { Cod = 512, Name = "LOURDES" });
                //    state.Cities.Add(new City { Cod = 586, Name = "MUTISCUA" });
                //    state.Cities.Add(new City { Cod = 612, Name = "OCAÑA" });
                //    state.Cities.Add(new City { Cod = 642, Name = "PAMPLONA" });
                //    state.Cities.Add(new City { Cod = 643, Name = "PAMPLONITA" });
                //    state.Cities.Add(new City { Cod = 714, Name = "PUERTO SANTANDER" });
                //    state.Cities.Add(new City { Cod = 734, Name = "RAGONVALIA" });
                //    state.Cities.Add(new City { Cod = 778, Name = "SALAZAR" });
                //    state.Cities.Add(new City { Cod = 799, Name = "SAN CALIXTO" });
                //    state.Cities.Add(new City { Cod = 804, Name = "SAN CAYETANO" });
                //    state.Cities.Add(new City { Cod = 890, Name = "SANTIAGO" });
                //    state.Cities.Add(new City { Cod = 898, Name = "SARDINATA" });
                //    state.Cities.Add(new City { Cod = 908, Name = "SILOS" });
                //    state.Cities.Add(new City { Cod = 976, Name = "TEORAMA" });
                //    state.Cities.Add(new City { Cod = 983, Name = "TIBÚ" });
                //    state.Cities.Add(new City { Cod = 997, Name = "TOLEDO" });
                //    state.Cities.Add(new City { Cod = 1056, Name = "VILLA CARO" });
                //    state.Cities.Add(new City { Cod = 1059, Name = "VILLA DEL ROSARIO" });
                //    state.Cities.Add(new City { Cod = 1097, Name = "ÁBREGO" });
                //    country.States.Add(state);
                //    state = new State { Cod = 63, Name = "QUINDIO", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 66, Name = "ARMENIA" });
                //    state.Cities.Add(new City { Cod = 123, Name = "BUENAVISTA" });
                //    state.Cities.Add(new City { Cod = 143, Name = "CALARCÁ" });
                //    state.Cities.Add(new City { Cod = 216, Name = "CIRCASIA" });
                //    state.Cities.Add(new City { Cod = 241, Name = "CORDOBÁ" });
                //    state.Cities.Add(new City { Cod = 335, Name = "FILANDIA" });
                //    state.Cities.Add(new City { Cod = 415, Name = "GÉNOVA" });
                //    state.Cities.Add(new City { Cod = 483, Name = "LA TEBAIDA" });
                //    state.Cities.Add(new City { Cod = 572, Name = "MONTENEGRO" });
                //    state.Cities.Add(new City { Cod = 664, Name = "PIJAO" });
                //    state.Cities.Add(new City { Cod = 730, Name = "QUIMBAYA" });
                //    state.Cities.Add(new City { Cod = 780, Name = "SALENTO" });
                //    country.States.Add(state);
                //    state = new State { Cod = 66, Name = "RISARALDA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 47, Name = "APÍA" });
                //    state.Cities.Add(new City { Cod = 77, Name = "BALBOA" });
                //    state.Cities.Add(new City { Cod = 97, Name = "BELÉN DE UMBRÍA" });
                //    state.Cities.Add(new City { Cod = 282, Name = "DOS QUEBRADAS" });
                //    state.Cities.Add(new City { Cod = 412, Name = "GUÁTICA" });
                //    state.Cities.Add(new City { Cod = 458, Name = "LA CELIA" });
                //    state.Cities.Add(new City { Cod = 495, Name = "LA VIRGINIA" });
                //    state.Cities.Add(new City { Cod = 543, Name = "MARSELLA" });
                //    state.Cities.Add(new City { Cod = 559, Name = "MISTRATÓ" });
                //    state.Cities.Add(new City { Cod = 657, Name = "PEREIRA" });
                //    state.Cities.Add(new City { Cod = 688, Name = "PUEBLO RICO" });
                //    state.Cities.Add(new City { Cod = 731, Name = "QUINCHÍA" });
                //    state.Cities.Add(new City { Cod = 882, Name = "SANTA ROSA DE CABAL" });
                //    state.Cities.Add(new City { Cod = 895, Name = "SANTUARIO" });
                //    country.States.Add(state);
                //    state = new State { Cod = 68, Name = "SANTANDER", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 9, Name = "AGUADA" });
                //    state.Cities.Add(new City { Cod = 16, Name = "ALBANIA" });
                //    state.Cities.Add(new City { Cod = 51, Name = "ARATOCA" });
                //    state.Cities.Add(new City { Cod = 82, Name = "BARBOSA" });
                //    state.Cities.Add(new City { Cod = 83, Name = "BARICHARA" });
                //    state.Cities.Add(new City { Cod = 85, Name = "BARRANCABERMEJA" });
                //    state.Cities.Add(new City { Cod = 103, Name = "BETULIA" });
                //    state.Cities.Add(new City { Cod = 112, Name = "BOLÍVAR" });
                //    state.Cities.Add(new City { Cod = 118, Name = "BUCARAMANGA" });
                //    state.Cities.Add(new City { Cod = 132, Name = "CABRERA" });
                //    state.Cities.Add(new City { Cod = 147, Name = "CALIFORNIA" });
                //    state.Cities.Add(new City { Cod = 161, Name = "CAPITANEJO" });
                //    state.Cities.Add(new City { Cod = 164, Name = "CARCASÍ" });
                //    state.Cities.Add(new City { Cod = 179, Name = "CEPITA" });
                //    state.Cities.Add(new City { Cod = 182, Name = "CERRITO" });
                //    state.Cities.Add(new City { Cod = 188, Name = "CHARALÁ" });
                //    state.Cities.Add(new City { Cod = 189, Name = "CHARTA" });
                //    state.Cities.Add(new City { Cod = 191, Name = "CHIMA" });
                //    state.Cities.Add(new City { Cod = 199, Name = "CHIPATÁ" });
                //    state.Cities.Add(new City { Cod = 215, Name = "CIMITARRA" });
                //    state.Cities.Add(new City { Cod = 230, Name = "CONCEPCIÓN" });
                //    state.Cities.Add(new City { Cod = 234, Name = "CONFINES" });
                //    state.Cities.Add(new City { Cod = 237, Name = "CONTRATACIÓN" });
                //    state.Cities.Add(new City { Cod = 243, Name = "COROMORO" });
                //    state.Cities.Add(new City { Cod = 265, Name = "CURITÍ" });
                //    state.Cities.Add(new City { Cod = 291, Name = "EL CARMEN" });
                //    state.Cities.Add(new City { Cod = 304, Name = "EL GUACAMAYO" });
                //    state.Cities.Add(new City { Cod = 311, Name = "EL PEÑON" });
                //    state.Cities.Add(new City { Cod = 314, Name = "EL PLAYÓN" });
                //    state.Cities.Add(new City { Cod = 327, Name = "ENCINO" });
                //    state.Cities.Add(new City { Cod = 328, Name = "ENCISO" });
                //    state.Cities.Add(new City { Cod = 342, Name = "FLORIDABLANCA" });
                //    state.Cities.Add(new City { Cod = 343, Name = "FLORIÁN" });
                //    state.Cities.Add(new City { Cod = 364, Name = "GALÁN" });
                //    state.Cities.Add(new City { Cod = 374, Name = "GIRÓN" });
                //    state.Cities.Add(new City { Cod = 380, Name = "GUACA" });
                //    state.Cities.Add(new City { Cod = 389, Name = "GUADALUPE" });
                //    state.Cities.Add(new City { Cod = 396, Name = "GUAPOTA" });
                //    state.Cities.Add(new City { Cod = 405, Name = "GUAVATÁ" });
                //    state.Cities.Add(new City { Cod = 409, Name = "GUEPSA" });
                //    state.Cities.Add(new City { Cod = 413, Name = "GÁMBITA" });
                //    state.Cities.Add(new City { Cod = 419, Name = "HATO" });
                //    state.Cities.Add(new City { Cod = 447, Name = "JESÚS MARÍA" });
                //    state.Cities.Add(new City { Cod = 448, Name = "JORDÁN" });
                //    state.Cities.Add(new City { Cod = 454, Name = "LA BELLEZA" });
                //    state.Cities.Add(new City { Cod = 474, Name = "LA PAZ" });
                //    state.Cities.Add(new City { Cod = 498, Name = "LANDÁZURI" });
                //    state.Cities.Add(new City { Cod = 499, Name = "LEBRIJA" });
                //    state.Cities.Add(new City { Cod = 511, Name = "LOS SANTOS" });
                //    state.Cities.Add(new City { Cod = 518, Name = "MACARAVITA" });
                //    state.Cities.Add(new City { Cod = 546, Name = "MATANZA" });
                //    state.Cities.Add(new City { Cod = 562, Name = "MOGOTES" });
                //    state.Cities.Add(new City { Cod = 563, Name = "MOLAGAVITA" });
                //    state.Cities.Add(new City { Cod = 588, Name = "MÁLAGA" });
                //    state.Cities.Add(new City { Cod = 611, Name = "OCAMONTE" });
                //    state.Cities.Add(new City { Cod = 613, Name = "OIBA" });
                //    state.Cities.Add(new City { Cod = 617, Name = "ONZAGA" });
                //    state.Cities.Add(new City { Cod = 636, Name = "PALMAR" });
                //    state.Cities.Add(new City { Cod = 638, Name = "PALMAS DEL SOCORRO" });
                //    state.Cities.Add(new City { Cod = 661, Name = "PIE DE CUESTA" });
                //    state.Cities.Add(new City { Cod = 666, Name = "PINCHOTE" });
                //    state.Cities.Add(new City { Cod = 691, Name = "PUENTE NACIONAL" });
                //    state.Cities.Add(new City { Cod = 709, Name = "PUERTO PARRA" });
                //    state.Cities.Add(new City { Cod = 717, Name = "PUERTO WILCHES" });
                //    state.Cities.Add(new City { Cod = 726, Name = "PÁRAMO" });
                //    state.Cities.Add(new City { Cod = 746, Name = "RIO NEGRO" });
                //    state.Cities.Add(new City { Cod = 766, Name = "SABANA DE TORRES" });
                //    state.Cities.Add(new City { Cod = 788, Name = "SAN ANDRÉS" });
                //    state.Cities.Add(new City { Cod = 794, Name = "SAN BENITO" });
                //    state.Cities.Add(new City { Cod = 813, Name = "SAN GÍL" });
                //    state.Cities.Add(new City { Cod = 817, Name = "SAN JOAQUÍN" });
                //    state.Cities.Add(new City { Cod = 819, Name = "SAN JOSÉ DE MIRANDA" });
                //    state.Cities.Add(new City { Cod = 844, Name = "SAN MIGUEL" });
                //    state.Cities.Add(new City { Cod = 863, Name = "SAN VICENTE DEL CHUCURÍ" });
                //    state.Cities.Add(new City { Cod = 868, Name = "SANTA BÁRBARA" });
                //    state.Cities.Add(new City { Cod = 874, Name = "SANTA HELENA DEL OPÓN" });
                //    state.Cities.Add(new City { Cod = 911, Name = "SIMACOTA" });
                //    state.Cities.Add(new City { Cod = 921, Name = "SOCORRO" });
                //    state.Cities.Add(new City { Cod = 937, Name = "SUAITA" });
                //    state.Cities.Add(new City { Cod = 942, Name = "SUCRE" });
                //    state.Cities.Add(new City { Cod = 947, Name = "SURATÁ" });
                //    state.Cities.Add(new City { Cod = 1000, Name = "TONA" });
                //    state.Cities.Add(new City { Cod = 1039, Name = "VALLE DE SAN JOSÉ" });
                //    state.Cities.Add(new City { Cod = 1052, Name = "VETAS" });
                //    state.Cities.Add(new City { Cod = 1066, Name = "VILLANUEVA" });
                //    state.Cities.Add(new City { Cod = 1077, Name = "VÉLEZ" });
                //    state.Cities.Add(new City { Cod = 1089, Name = "ZAPATOCA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 70, Name = "SUCRE", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 124, Name = "BUENAVISTA" });
                //    state.Cities.Add(new City { Cod = 137, Name = "CAIMITO" });
                //    state.Cities.Add(new City { Cod = 186, Name = "CHALÁN" });
                //    state.Cities.Add(new City { Cod = 226, Name = "COLOSÓ (RICAURTE)" });
                //    state.Cities.Add(new City { Cod = 244, Name = "COROZAL" });
                //    state.Cities.Add(new City { Cod = 249, Name = "COVEÑAS" });
                //    state.Cities.Add(new City { Cod = 317, Name = "EL ROBLE" });
                //    state.Cities.Add(new City { Cod = 363, Name = "GALERAS (NUEVA GRANADA)" });
                //    state.Cities.Add(new City { Cod = 398, Name = "GUARANDA" });
                //    state.Cities.Add(new City { Cod = 487, Name = "LA UNIÓN" });
                //    state.Cities.Add(new City { Cod = 509, Name = "LOS PALMITOS" });
                //    state.Cities.Add(new City { Cod = 526, Name = "MAJAGUAL" });
                //    state.Cities.Add(new City { Cod = 578, Name = "MORROA" });
                //    state.Cities.Add(new City { Cod = 624, Name = "OVEJAS" });
                //    state.Cities.Add(new City { Cod = 640, Name = "PALMITO" });
                //    state.Cities.Add(new City { Cod = 785, Name = "SAMPUÉS" });
                //    state.Cities.Add(new City { Cod = 795, Name = "SAN BENITO ABAD" });
                //    state.Cities.Add(new City { Cod = 827, Name = "SAN JUAN DE BETULIA" });
                //    state.Cities.Add(new City { Cod = 839, Name = "SAN MARCOS" });
                //    state.Cities.Add(new City { Cod = 847, Name = "SAN ONOFRE" });
                //    state.Cities.Add(new City { Cod = 852, Name = "SAN PEDRO" });
                //    state.Cities.Add(new City { Cod = 914, Name = "SINCELEJO" });
                //    state.Cities.Add(new City { Cod = 915, Name = "SINCÉ" });
                //    state.Cities.Add(new City { Cod = 943, Name = "SUCRE" });
                //    state.Cities.Add(new City { Cod = 998, Name = "TOLÚ" });
                //    state.Cities.Add(new City { Cod = 999, Name = "TOLÚ VIEJO" });
                //    country.States.Add(state);
                //    state = new State { Cod = 73, Name = "TOLIMA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 25, Name = "ALPUJARRA" });
                //    state.Cities.Add(new City { Cod = 29, Name = "ALVARADO" });
                //    state.Cities.Add(new City { Cod = 32, Name = "AMBALEMA" });
                //    state.Cities.Add(new City { Cod = 43, Name = "ANZOÁTEGUI" });
                //    state.Cities.Add(new City { Cod = 67, Name = "ARMERO (GUAYABAL)" });
                //    state.Cities.Add(new City { Cod = 70, Name = "ATACO" });
                //    state.Cities.Add(new City { Cod = 138, Name = "CAJAMARCA" });
                //    state.Cities.Add(new City { Cod = 166, Name = "CARMEN DE APICALÁ" });
                //    state.Cities.Add(new City { Cod = 175, Name = "CASABIANCA" });
                //    state.Cities.Add(new City { Cod = 187, Name = "CHAPARRAL" });
                //    state.Cities.Add(new City { Cod = 223, Name = "COELLO" });
                //    state.Cities.Add(new City { Cod = 250, Name = "COYAIMA" });
                //    state.Cities.Add(new City { Cod = 263, Name = "CUNDAY" });
                //    state.Cities.Add(new City { Cod = 280, Name = "DOLORES" });
                //    state.Cities.Add(new City { Cod = 331, Name = "ESPINAL" });
                //    state.Cities.Add(new City { Cod = 333, Name = "FALAN" });
                //    state.Cities.Add(new City { Cod = 337, Name = "FLANDES" });
                //    state.Cities.Add(new City { Cod = 349, Name = "FRESNO" });
                //    state.Cities.Add(new City { Cod = 395, Name = "GUAMO" });
                //    state.Cities.Add(new City { Cod = 424, Name = "HERVEO" });
                //    state.Cities.Add(new City { Cod = 427, Name = "HONDA" });
                //    state.Cities.Add(new City { Cod = 428, Name = "IBAGUÉ" });
                //    state.Cities.Add(new City { Cod = 429, Name = "ICONONZO" });
                //    state.Cities.Add(new City { Cod = 514, Name = "LÉRIDA" });
                //    state.Cities.Add(new City { Cod = 515, Name = "LÍBANO" });
                //    state.Cities.Add(new City { Cod = 540, Name = "MARIQUITA" });
                //    state.Cities.Add(new City { Cod = 552, Name = "MELGAR" });
                //    state.Cities.Add(new City { Cod = 583, Name = "MURILLO" });
                //    state.Cities.Add(new City { Cod = 592, Name = "NATAGAIMA" });
                //    state.Cities.Add(new City { Cod = 621, Name = "ORTEGA" });
                //    state.Cities.Add(new City { Cod = 641, Name = "PALOCABILDO" });
                //    state.Cities.Add(new City { Cod = 662, Name = "PIEDRAS" });
                //    state.Cities.Add(new City { Cod = 673, Name = "PLANADAS" });
                //    state.Cities.Add(new City { Cod = 683, Name = "PRADO" });
                //    state.Cities.Add(new City { Cod = 721, Name = "PURIFICACIÓN" });
                //    state.Cities.Add(new City { Cod = 747, Name = "RIOBLANCO" });
                //    state.Cities.Add(new City { Cod = 754, Name = "RONCESVALLES" });
                //    state.Cities.Add(new City { Cod = 757, Name = "ROVIRA" });
                //    state.Cities.Add(new City { Cod = 779, Name = "SALDAÑA" });
                //    state.Cities.Add(new City { Cod = 792, Name = "SAN ANTONIO" });
                //    state.Cities.Add(new City { Cod = 835, Name = "SAN LUIS" });
                //    state.Cities.Add(new City { Cod = 875, Name = "SANTA ISABEL" });
                //    state.Cities.Add(new City { Cod = 954, Name = "SUÁREZ" });
                //    state.Cities.Add(new City { Cod = 1040, Name = "VALLE DE SAN JUAN" });
                //    state.Cities.Add(new City { Cod = 1046, Name = "VENADILLO" });
                //    state.Cities.Add(new City { Cod = 1062, Name = "VILLAHERMOSA" });
                //    state.Cities.Add(new City { Cod = 1069, Name = "VILLARRICA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 76, Name = "VALLE DEL CAUCA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 19, Name = "ALCALÁ" });
                //    state.Cities.Add(new City { Cod = 35, Name = "ANDALUCÍA" });
                //    state.Cities.Add(new City { Cod = 42, Name = "ANSERMANUEVO" });
                //    state.Cities.Add(new City { Cod = 62, Name = "ARGELIA" });
                //    state.Cities.Add(new City { Cod = 113, Name = "BOLÍVAR" });
                //    state.Cities.Add(new City { Cod = 120, Name = "BUENAVENTURA" });
                //    state.Cities.Add(new City { Cod = 127, Name = "BUGA" });
                //    state.Cities.Add(new City { Cod = 128, Name = "BUGALAGRANDE" });
                //    state.Cities.Add(new City { Cod = 136, Name = "CAICEDONIA" });
                //    state.Cities.Add(new City { Cod = 148, Name = "CALIMA (DARIÉN)" });
                //    state.Cities.Add(new City { Cod = 150, Name = "CALÍ" });
                //    state.Cities.Add(new City { Cod = 157, Name = "CANDELARIA" });
                //    state.Cities.Add(new City { Cod = 173, Name = "CARTAGO" });
                //    state.Cities.Add(new City { Cod = 277, Name = "DAGUA" });
                //    state.Cities.Add(new City { Cod = 288, Name = "EL CAIRO" });
                //    state.Cities.Add(new City { Cod = 295, Name = "EL CERRITO" });
                //    state.Cities.Add(new City { Cod = 302, Name = "EL DOVIO" });
                //    state.Cities.Add(new City { Cod = 325, Name = "EL ÁGUILA" });
                //    state.Cities.Add(new City { Cod = 341, Name = "FLORIDA" });
                //    state.Cities.Add(new City { Cod = 370, Name = "GINEBRA" });
                //    state.Cities.Add(new City { Cod = 382, Name = "GUACARÍ" });
                //    state.Cities.Add(new City { Cod = 441, Name = "JAMUNDÍ" });
                //    state.Cities.Add(new City { Cod = 460, Name = "LA CUMBRE" });
                //    state.Cities.Add(new City { Cod = 488, Name = "LA UNIÓN" });
                //    state.Cities.Add(new City { Cod = 494, Name = "LA VICTORIA" });
                //    state.Cities.Add(new City { Cod = 610, Name = "OBANDO" });
                //    state.Cities.Add(new City { Cod = 639, Name = "PALMIRA" });
                //    state.Cities.Add(new City { Cod = 682, Name = "PRADERA" });
                //    state.Cities.Add(new City { Cod = 742, Name = "RESTREPO" });
                //    state.Cities.Add(new City { Cod = 748, Name = "RIOFRÍO" });
                //    state.Cities.Add(new City { Cod = 753, Name = "ROLDANILLO" });
                //    state.Cities.Add(new City { Cod = 853, Name = "SAN PEDRO" });
                //    state.Cities.Add(new City { Cod = 904, Name = "SEVILLA" });
                //    state.Cities.Add(new City { Cod = 1004, Name = "TORO" });
                //    state.Cities.Add(new City { Cod = 1008, Name = "TRUJILLO" });
                //    state.Cities.Add(new City { Cod = 1011, Name = "TULÚA" });
                //    state.Cities.Add(new City { Cod = 1027, Name = "ULLOA" });
                //    state.Cities.Add(new City { Cod = 1051, Name = "VERSALLES" });
                //    state.Cities.Add(new City { Cod = 1055, Name = "VIJES" });
                //    state.Cities.Add(new City { Cod = 1086, Name = "YOTOCO" });
                //    state.Cities.Add(new City { Cod = 1087, Name = "YUMBO" });
                //    state.Cities.Add(new City { Cod = 1092, Name = "ZARZAL" });
                //    country.States.Add(state);
                //    state = new State { Cod = 81, Name = "ARAUCA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 52, Name = "ARAUCA" });
                //    state.Cities.Add(new City { Cod = 53, Name = "ARAUQUITA" });
                //    state.Cities.Add(new City { Cod = 251, Name = "CRAVO NORTE" });
                //    state.Cities.Add(new City { Cod = 345, Name = "FORTÚL" });
                //    state.Cities.Add(new City { Cod = 712, Name = "PUERTO RONDÓN" });
                //    state.Cities.Add(new City { Cod = 897, Name = "SARAVENA" });
                //    state.Cities.Add(new City { Cod = 961, Name = "TAME" });
                //    country.States.Add(state);
                //    state = new State { Cod = 85, Name = "CASANARE", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 11, Name = "AGUAZUL" });
                //    state.Cities.Add(new City { Cod = 210, Name = "CHÁMEZA" });
                //    state.Cities.Add(new City { Cod = 420, Name = "HATO COROZAL" });
                //    state.Cities.Add(new City { Cod = 481, Name = "LA SALINA" });
                //    state.Cities.Add(new City { Cod = 535, Name = "MANÍ" });
                //    state.Cities.Add(new City { Cod = 574, Name = "MONTERREY" });
                //    state.Cities.Add(new City { Cod = 607, Name = "NUNCHÍA" });
                //    state.Cities.Add(new City { Cod = 620, Name = "OROCUÉ" });
                //    state.Cities.Add(new City { Cod = 651, Name = "PAZ DE ARIPORO" });
                //    state.Cities.Add(new City { Cod = 680, Name = "PORE" });
                //    state.Cities.Add(new City { Cod = 736, Name = "RECETOR" });
                //    state.Cities.Add(new City { Cod = 770, Name = "SABANALARGA" });
                //    state.Cities.Add(new City { Cod = 838, Name = "SAN LUÍS DE PALENQUE" });
                //    state.Cities.Add(new City { Cod = 955, Name = "SÁCAMA" });
                //    state.Cities.Add(new City { Cod = 969, Name = "TAURAMENA" });
                //    state.Cities.Add(new City { Cod = 1007, Name = "TRINIDAD" });
                //    state.Cities.Add(new City { Cod = 1021, Name = "TÁMARA" });
                //    state.Cities.Add(new City { Cod = 1067, Name = "VILLANUEVA" });
                //    state.Cities.Add(new City { Cod = 1085, Name = "YOPAL" });
                //    country.States.Add(state);
                //    state = new State { Cod = 86, Name = "PUTUMAYO", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 227, Name = "COLÓN" });
                //    state.Cities.Add(new City { Cod = 561, Name = "MOCOA" });
                //    state.Cities.Add(new City { Cod = 619, Name = "ORITO" });
                //    state.Cities.Add(new City { Cod = 693, Name = "PUERTO ASÍS" });
                //    state.Cities.Add(new City { Cod = 696, Name = "PUERTO CAICEDO" });
                //    state.Cities.Add(new City { Cod = 702, Name = "PUERTO GUZMÁN" });
                //    state.Cities.Add(new City { Cod = 703, Name = "PUERTO LEGUÍZAMO" });
                //    state.Cities.Add(new City { Cod = 812, Name = "SAN FRANCISCO" });
                //    state.Cities.Add(new City { Cod = 845, Name = "SAN MIGUEL" });
                //    state.Cities.Add(new City { Cod = 891, Name = "SANTIAGO" });
                //    state.Cities.Add(new City { Cod = 907, Name = "SIBUNDOY" });
                //    state.Cities.Add(new City { Cod = 1041, Name = "VALLE DEL GUAMUEZ" });
                //    state.Cities.Add(new City { Cod = 1060, Name = "VILLAGARZÓN" });
                //    country.States.Add(state);
                //    state = new State { Cod = 88, Name = "ARCHIPIÉLAGO DE SAN ANDRÉS, PROVIDENCIA Y SANTA CATALINA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 685, Name = "PROVIDENCIA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 91, Name = "AMAZONAS", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 503, Name = "LETICIA" });
                //    state.Cities.Add(new City { Cod = 708, Name = "PUERTO NARIÑO" });
                //    country.States.Add(state);
                //    state = new State { Cod = 94, Name = "GUAINÍA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 433, Name = "INÍRIDA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 95, Name = "GUAVIARE", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 142, Name = "CALAMAR" });
                //    state.Cities.Add(new City { Cod = 315, Name = "EL RETORNO" });
                //    state.Cities.Add(new City { Cod = 557, Name = "MIRAFLORES" });
                //    state.Cities.Add(new City { Cod = 824, Name = "SAN JOSÉ DEL GUAVIARE" });
                //    country.States.Add(state);
                //    state = new State { Cod = 97, Name = "VAUPÉS", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 174, Name = "CARURÚ" });
                //    state.Cities.Add(new City { Cod = 560, Name = "MITÚ" });
                //    state.Cities.Add(new City { Cod = 964, Name = "TARAIRA" });
                //    country.States.Add(state);
                //    state = new State { Cod = 99, Name = "VICHADA", Cities = new List<City>() };
                //    state.Cities.Add(new City { Cod = 260, Name = "CUMARIBO" });
                //    state.Cities.Add(new City { Cod = 480, Name = "LA PRIMAVERA" });
                //    state.Cities.Add(new City { Cod = 697, Name = "PUERTO CARREÑO" });
                //    state.Cities.Add(new City { Cod = 886, Name = "SANTA ROSALÍA" });
                //    country.States.Add(state);
                //    _context.Countries.Add(country);
                //    await _context.SaveChangesAsync();
                //}
            }
        }
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
                    Name = "Inversiones quimicas HED",
                    Nit = "890900652-3",
                    City = await _context.Cities.FirstOrDefaultAsync(x => x.Name.ToUpper().Equals("MEDELLÍN")),
                    Status = true,
                });
                _context.Companies.Add(new Company
                {
                    Id = 0,
                    Name = "Comfenalco Antioquia",
                    Nit = "890900842",
                    City = await _context.Cities.FirstOrDefaultAsync(x => x.Name.ToUpper().Equals("MEDELLÍN")),
                    Status = true,
                });
                _context.Companies.Add(new Company
                {
                    Id = 0,
                    Name = "ATI SAS",
                    Nit = "830118667-1",
                    City = await _context.Cities.FirstOrDefaultAsync(x => x.Name.ToUpper().Equals("DUITAMA")),
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
                    _context.Processes.Add(new Process { Description = "Talento Humano", Status = true, CompanyId = company.Id, Company = company });
                    _context.Processes.Add(new Process { Description = "Planta pinturas", Status = true, CompanyId = company.Id, Company = company });
                    _context.Processes.Add(new Process { Description = "Planta agroquimicos", Status = true, CompanyId = company.Id, Company = company });
                }
                company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "830118667-1");
                if (company != null)
                {
                    _context.Processes.Add(new Process { Description = "Gestion Humana", Status = true, CompanyId = company.Id, Company = company });
                    _context.Processes.Add(new Process { Description = "HSEQ", Status = true, CompanyId = company.Id, Company = company });
                    _context.Processes.Add(new Process { Description = "Financiera", Status = true, CompanyId = company.Id, Company = company });
                }
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckOccupationsAsync()
        {
            if (!_context.Occupations.Any())
            {
                var process = await _context.Processes.FirstOrDefaultAsync(p => p.Description.Equals("Talento Humano"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Description = "Analista Gestion Humana", ProcessId = process.Id, Process = process, Status = true });
                }
                process = await _context.Processes.FirstOrDefaultAsync(p => p.Description.Equals("Planta pinturas"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Description = "Operario pinturas", ProcessId = process.Id, Process = process, Status = true });
                    _context.Occupations.Add(new Occupation { Description = "Coordinador Pinturas", ProcessId = process.Id, Process = process, Status = true });
                }
                process = await _context.Processes.FirstOrDefaultAsync(p => p.Description.Equals("Planta agroquimicos"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Description = "Auxiliar Pinturas", ProcessId = process.Id, Process = process, Status = true });
                    _context.Occupations.Add(new Occupation { Description = "Operario Agroquimicos", ProcessId = process.Id, Process = process, Status = true });
                }
                process = await _context.Processes.FirstOrDefaultAsync(p => p.Description.Equals("Gestion Humana"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Description = "Lider Gestión Humana", ProcessId = process.Id, Process = process, Status = true });
                    _context.Occupations.Add(new Occupation { Description = "Lider Administrativo", ProcessId = process.Id, Process = process, Status = true });
                }
                process = await _context.Processes.FirstOrDefaultAsync(p => p.Description.Equals("HSEQ"));
                if (process != null)
                {
                    _context.Occupations.Add(new Occupation { Description = "Coordinador Hseq", ProcessId = process.Id, Process = process, Status = true });
                    _context.Occupations.Add(new Occupation { Description = "Coordinador Psev", ProcessId = process.Id, Process = process, Status = true });
                }
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckFormationsAsync()
        {
            if (!_context.Formations.Any())
            {
                var occupation = await _context.Occupations.FirstOrDefaultAsync(o => o.Description.Equals("Analista Gestion Humana"));
                if (occupation != null)
                {
                    var company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "890900652-3");
                    if (company != null)
                    {
                        _context.Formations.Add(new Formation { Description = "Inducción del personal.", Status = true, CompanyId = company.Id, Company = company }); ;
                        _context.Formations.Add(new Formation { Description = "Formación en seguridad.", Status = true, CompanyId = company.Id, Company = company });
                    }
                    company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "830118667-1");
                    if (company != null)
                    {
                        _context.Formations.Add(new Formation { Description = "Riesgo Biológico.", Status = true, CompanyId = company.Id, Company = company }); ;
                        _context.Formations.Add(new Formation { Description = "Manejo de Extintores.", Status = true, CompanyId = company.Id, Company = company });
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
                    _context.Topics.Add(new Topic { Description = "Sistema Integrado de Gestión.", Details = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando esencialmente igual al original. Fue popularizado en los 60s con la creación de las hojas \"Letraset\", las cuales contenian pasajes de Lorem Ipsum, y más recientemente con software de autoedición, como por ejemplo Aldus PageMaker, el cual incluye versiones de Lorem Ipsum.", Status = true, CompanyId = company.Id, Company = company });
                    _context.Topics.Add(new Topic { Description = "Medio ambiente", Status = true, Details = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando esencialmente igual al original. Fue popularizado en los 60s con la creación de las hojas \"Letraset\", las cuales contenian pasajes de Lorem Ipsum, y más recientemente con software de autoedición, como por ejemplo Aldus PageMaker, el cual incluye versiones de Lorem Ipsum.", CompanyId = company.Id, Company = company });
                }
                company = await _context.Companies.FirstOrDefaultAsync(x => x.Nit == "830118667-1");
                if (company != null)
                {
                    _context.Topics.Add(new Topic { Description = "Sistema Integrado de Gestión.", Details = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando esencialmente igual al original. Fue popularizado en los 60s con la creación de las hojas \"Letraset\", las cuales contenian pasajes de Lorem Ipsum, y más recientemente con software de autoedición, como por ejemplo Aldus PageMaker, el cual incluye versiones de Lorem Ipsum.", Status = true, CompanyId = company.Id, Company = company });
                    _context.Topics.Add(new Topic { Description = "Medio ambiente", Status = true, Details = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando esencialmente igual al original. Fue popularizado en los 60s con la creación de las hojas \"Letraset\", las cuales contenian pasajes de Lorem Ipsum, y más recientemente con software de autoedición, como por ejemplo Aldus PageMaker, el cual incluye versiones de Lorem Ipsum.", CompanyId = company.Id, Company = company });
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
                var process = await _context.Processes.FirstOrDefaultAsync(o => o.Description.Equals("Talento Humano"));
                if (process != null)
                {
                    _context.Trainings.Add(new Training { Description = "Inducción al sistema integrado de gestión.", Observation = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando esencialmente igual al original. Fue popularizado en los 60s con la creación de las hojas \"Letraset\", las cuales contenian pasajes de Lorem Ipsum, y más recientemente con software de autoedición, como por ejemplo Aldus PageMaker, el cual incluye versiones de Lorem Ipsum.", Type = true, Duration = 60, ProcessId = process.Id, Process = process, Status = true, });
                    _context.Trainings.Add(new Training { Description = "Manejo integral de residuos solidos.", Observation = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando esencialmente igual al original. Fue popularizado en los 60s con la creación de las hojas \"Letraset\", las cuales contenian pasajes de Lorem Ipsum, y más recientemente con software de autoedición, como por ejemplo Aldus PageMaker, el cual incluye versiones de Lorem Ipsum.", Type = true, Duration = 60, ProcessId = process.Id, Process = process, Status = true });

                    await _context.SaveChangesAsync();
                }
                process = await _context.Processes.FirstOrDefaultAsync(o => o.Description.Equals("HSEQ"));
                if (process != null)
                {
                    _context.Trainings.Add(new Training { Description = "Inducción al sistema integrado de gestión.", Observation = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando esencialmente igual al original. Fue popularizado en los 60s con la creación de las hojas \"Letraset\", las cuales contenian pasajes de Lorem Ipsum, y más recientemente con software de autoedición, como por ejemplo Aldus PageMaker, el cual incluye versiones de Lorem Ipsum.", Type = true, Duration = 60, ProcessId = process.Id, Process = process, Status = true, });
                    _context.Trainings.Add(new Training { Description = "Manejo integral de residuos solidos.", Observation = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando esencialmente igual al original. Fue popularizado en los 60s con la creación de las hojas \"Letraset\", las cuales contenian pasajes de Lorem Ipsum, y más recientemente con software de autoedición, como por ejemplo Aldus PageMaker, el cual incluye versiones de Lorem Ipsum.", Type = true, Duration = 60, ProcessId = process.Id, Process = process, Status = true });

                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task CheckRequestStatusAsync()
        {
            if (!_context.RequestStatus.Any())
            {
                _context.RequestStatus.Add(new RequestStatus { Name = "Registered", Description = "Registrada", Status = true });
                _context.RequestStatus.Add(new RequestStatus { Name = "Authorized", Description = "Autorizada", Status = true });
                _context.RequestStatus.Add(new RequestStatus { Name = "Refused", Description = "Rechazada", Status = true });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckSessionStatusAsync()
        {
            if (!_context.SessionStatus.Any())
            {
                _context.SessionStatus.Add(new SessionStatus { Name = "Scheduled", Description = "Programada", Status = true });
                _context.SessionStatus.Add(new SessionStatus { Name = "Cancelled", Description = "Cancelada", Status = true });
                _context.SessionStatus.Add(new SessionStatus { Name = "Complete", Description = "Completada", Status = true });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckSessionInscriptionStatusAsync()
        {
            if (!_context.SessionInscriptionStatus.Any())
            {
                _context.SessionInscriptionStatus.Add(new SessionInscriptionStatus { Name = "Registered", Description = "Matriculado", Status = true });
                _context.SessionInscriptionStatus.Add(new SessionInscriptionStatus { Name = "Refused", Description = "Rechazado", Status = true });
                _context.SessionInscriptionStatus.Add(new SessionInscriptionStatus { Name = "Confirmed", Description = "Confirmado", Status = true });
                _context.SessionInscriptionStatus.Add(new SessionInscriptionStatus { Name = "Cancelled", Description = "Cancelado", Status = true });
                await _context.SaveChangesAsync();
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
        private async Task CheckUseCompanysrAsync()
        {
            //ATI
            await CheckUserAsync("830118667-1", "1052381821", "DAVID ALFONSO", "BLANCO CAMARGO", "davidblancoc@yopmail.com", "3108145253", "CARRERA 6 No. 16 - 83", "DUITAMA", 40, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "1053615655", "MARIA FERNANDA", "ALVARADO ORJUELA", "marcelaorjuela74@yopmail.com", "3124453130", "CARRERA 24D N° 11A-01", "DUITAMA", 21, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "1007707804", "CAMILO ANDRES", "GUERRERO GUERRERO", "camiloguerrero656@yopmail.com", "3124453130", "AVENIDA CIRCUNVALAR 20 68", "DUITAMA", 22, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "1052413439", "ANDRES YOVANNY", "PEÑALOZA PUERTO", "andrespuerto1713@yopmail.com", "3004148157", "CARRERA 40 22 77", "DUITAMA", 22, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "23690684", "ROSIBEL", "VELANDIA", "sierrarosibel424@yopmail.com", "3143701420", "VEREDA LLANO DEL ARBOL", "DUITAMA", 27, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "1118552779", "CLAUDIA PAOLA", "SORIANO AREVALO", "claudiasoriano9211@yopmail.com", "3182299213", "CALLE 16 A 25 35", "DUITAMA", 38, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "4119484", "JULIO ORLANDO", "UNIVIO AVELLA", "julioorlando2017univio@yopmail.com", "3123378732", "CARRERA  2A # 4- 05", "DUITAMA", 27, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "1054680699", "MARIA CAMILA", "JIMENEZ CORREDOR", "mcjimenez1054@yopmail.com", "3202512572", "CARRERA 4 No 18 O5", "DUITAMA", 22, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "46450396", "MILENA ESPERANZA", "PORRAS  AMAYA", "milenaporras46@yopmail.com", "3102369645", "CARRERAA 25 20 90 APTO 401", "DUITAMA", 20, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "1005451893", "EDIER JULIAN", "QUIROGA RODRIGUEZ", "edierquiroga2018@yopmail.com", "3112716658", "Calle 52 a # 94 - linea ferrea", "DUITAMA", 9, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "40042175", "BLANCA PATRICIA", "BERNAL PARDO", "blancabernal.2719@yopmail.com", "3105672436", "TR 27 VEREDA TOIBITA", "DUITAMA", 27, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "23589136", "LUZ ESTELLA", "RODRIGUEZ GUERRERO", "luesroc@yopmail.com", "3124579997", "CRA 14 #2-20", "DUITAMA", 8, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "4191541", "JOSE EFRAIN", "RUIZ SUAREZ", "efrainruiz2360@yopmail.com", "3208870277", "CL 26 20 61", "DUITAMA", 27, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "7175771", "HANDERSON RUBIEL ", "SALCEDO TREJOS", "handersonrst@yopmail.com", "3102453945", "CR 8 62 34", "DUITAMA", 39, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "1052400019", "LEIDY CAROLINA", "OLIVARES ARAQUE", "carolina1052@yopmail.com", "3203348069", "CR 25 A 11 31", "DUITAMA", 7, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "46681790", "MIRIAN YANED", "CAMARGO FONSECA", "yaned.201@yopmail.com", "3212838662", "CL 18 27 12", "DUITAMA", 27, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "23857812", "OLGA SOFIA", "ORGANISTA MUNOZ", "olsofy@yopmail.com", "3137220737", "CL 20 22 27", "DUITAMA", 33, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "46682393", "CLAUDIA ANDREA", "CHAVARRIA RONDON", "julife0408@yopmail.com", "3102658769", "MZ D CA 15 SAN DANIEL", "DUITAMA", 16, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "40032731", "CLAUDIA PATRICIA", "RODRIGUEZ GUASGUITA", "clagua9@yopmail.com", "3158654763", "SEC TRES ESQUINAS", "DUITAMA", 17, UserType.Employee, "Inicio123*");
            await CheckUserAsync("830118667-1", "46454633", "CLAUDIA MILENA", "REYES GIL", "cmreyesg@yopmail.com", "3127874784", "CR 17 23 43", "DUITAMA", 13, UserType.Employee, "Inicio123*");

            //INVESA
            await CheckUserAsync("890900652-3", "1007420186", "ANGEL RODOLFO", "MORENO CAMARGO", "morerodolfoangel1234@yopmail.com", "3003450627", "Vereda novaré", "MEDELLÍN", 1, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1002396460", "LORENA ALEXANDRA", "SANCHEZ QUITO", "lorenagarcho03@yopmail.com", "3002829531", "CALLE 12a N 5-28 BARRIO JORDAN", "MEDELLÍN", 2, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1002309076", "DAVID MATEO", "MARTINEZ QUICA", "davidmatius27@yopmail.com", "3003170841", "CARRERA 12 11 40 SUR", "MEDELLÍN", 3, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "9636381", "HECTOR FAVIO", "CHAPARRO HERRERA", "hefachahe51@yopmail.com", "3003951910", "VEREDA CORAZON", "MEDELLÍN", 4, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "74082667", "WILLIAM REYNALDO", "ACEVEDO ROJAS", "williamacevedo1983@yopmail.com", "3003297534", "CARRERA 3 6 09 - BARRIO SAN PEDRO", "MEDELLÍN", 5, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1058461508", "EDWIN DAVID", "CHOCONTA LOPEZ", "davidchl2796@yopmail.com", "3007705489", "VEREDA TOTA SECTOR BAJO", "MEDELLÍN", 10, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1052359048", "MARIA HERMENCIA", "SANCHEZ", "riverasanchezmaria@yopmail.com", "3004792805", "VEREDA VALLE", "MEDELLÍN", 11, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1002702250", "VELA", "AMAYA YAMITH", "yamithvelaamaya@yopmail.com", "3008511029", "VEREDA SOAQUIRA", "MEDELLÍN", 12, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "53006936", "FRANCY NELLY", "SALAMANCA MENDOZA", "francysalamanca966@yopmail.com", "3009978481", "CARRERA 3 5-03", "MEDELLÍN", 14, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1054146174", "JERSON ALEXANDER", "SUPELANO LOPEZ", "jersonalexlopez2017@yopmail.com", "3003253609", "CALLE 20A No. 20-22", "MEDELLÍN", 15, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1054683589", "NIXON DANIEL", "REYES GUTIERREZ", "rnixondaniel@yopmail.com", "3004103486", "CARRERA 1e #1b -09 BARRIO COAMIGOS", "MEDELLÍN", 18, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1053614025", "SANDRA CATALINA", "BENITEZ MARTINEZ", "catabenitez0512@yopmail.com", "3008146656", "CARRERA 22 20A 37", "MEDELLÍN", 19, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "93437648", "HAITHER", "ARELLANOS QUINTANA", "haitherarellano29@yopmail.com", "3003478484", "VEREDA LA FIEBRE FINCA AQUITOY", "MEDELLÍN", 23, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1002602616", "MAURI ALEXANDER", "BOSIGA MARTÍNEZ", "mauryalex315@yopmail.com", "3002365969", "CARRERA 13 No 9 32", "MEDELLÍN", 24, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1052412442", "YERALDIN", "BARRERA VALDERRAMA", "yeralbarrera29@yopmail.com", "3004467895", "CALLE 14 12 14", "MEDELLÍN", 25, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1050200121", "YUDI ALEJANDRA", "VARGAS RIVERA", "riveraaleja918@yopmail.com", "3008878282", "CALLE 18 SUR 7 47 BARRIO VENECIA", "MEDELLÍN", 26, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1057597300", "JEYSON  ANDRES", "PEREZ BARRAGAN", "jeysonpb21@yopmail.com", "3008556894", "CARRERA 14A # 6-22", "MEDELLÍN", 28, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "74188653", "HIDELFONSO", "PEREZ PEREZ", "perilde18@yopmail.com", "3004693818", "CALLE 34 A No. 10 BIS - 93", "MEDELLÍN", 31, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1053538153", "JOSE AGUSTIN", "AVELLA", "janitsuga71@yopmail.com", "3003850598", "VEREDA SAN MIGUEL", "MEDELLÍN", 32, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1054679772", "EDWIN RICARDO", "CASTRO REYES", "ricardo_1990@yopmail.com", "3003003319", "CARRERA 1 E N 18 09", "MEDELLÍN", 35, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1058058758", "JHON JAIRO", "AGUIRRE TEJEDOR", "jhonjairo1994@yopmail.com", "3004698163", "CARRERA  2 # 5-114", "MEDELLÍN", 36, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "7334295", "JOSE EULICES", "RUIZ RUIZ", "joseeulisesruiz2021@yopmail.com", "3004050272", "ALMEIDA VDA TONA", "MEDELLÍN", 37, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1054659066", "LUIS ESTEBAN", "BEJARANO DAZA", "fj46628@yopmail.com", "3003481346", "VDA LA VEGA", "MEDELLÍN", 15, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "74180909", "WILLIAM ARMANDO", "RUIZ ALBARRACIN", "wara1974@yopmail.com", "3004730534", "CALLE 7A 14A-13", "MEDELLÍN", 18, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1049646333", "ANDRES MAURICIO", "DIAZ JIMENEZ", "amdj960613@yopmail.com", "3002516341", "TRANSVERSAL 17B No. 32- 17", "MEDELLÍN", 19, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1057586290", "LIZETH VANESSA", "PALACIOS NOVA", "lizethvanessapalaciosnova@yopmail.com", "3008396375", "calle 21 11a 51", "MEDELLÍN", 23, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1007196568", "SANDRA DANIELA", "SALAMANCA PORRAS", "daniiela3678@yopmail.com", "3002354879", "CARRERA 20a N° 3-42", "MEDELLÍN", 24, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1118540872", "JOSE ANDRES", "ROJAS CARREÑO", "joseandresrojasca@yopmail.com", "3004002730", "vereda ochica la montaña", "MEDELLÍN", 25, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1002300000", "CARLOS DANIEL", "ALDANA RODRIGUEZ", "alcardaniel@yopmail.com", "3003965204", "VRD. LLANO GRANDE", "MEDELLÍN", 26, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "1054681341", "JHON FREDY", "CORTES LEON", "jhonf1693@yopmail.com", "3005260357", "CALLE 95 A SUR No. 14 - 28", "MEDELLÍN", 28, UserType.Employee, "Inicio123*");
            await CheckUserAsync("890900652-3", "4081282", "MANUEL ANTONIO", "MOLINA ROJAS", "mmolinarojas37@yopmail.com", "3002328611", "CENTRO", "MEDELLÍN", 31, UserType.Employee, "Inicio123*");
        }
        private async Task CheckUserAsync(string? nit, string document, string firstName, string lastName, string email, string? phoneNumber, string address, string city, int? occupationId, UserType userType, string pass)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Address = address,
                    Document = document,
                    DocumentType = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Name == "CC"),
                    City = await _context.Cities.FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(city.ToUpper())),
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
                if (occupationId != null)
                {
                    var occupation = await _context.Occupations.FirstOrDefaultAsync(x => x.Id == occupationId);
                    if (occupation != null)
                    {
                        user.OccupationId = occupationId;
                        user.Occupation = occupation;
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
