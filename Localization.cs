using System.Text.RegularExpressions;

namespace COLocalization {
    public class Localization {

        /// <summary>
        /// The current <see cref="Languages"/> set for default translating
        /// </summary>
        public static Languages CURRENT_LANGUAGE = Languages.en_us;

        private static string DEFAULT_LOCALIZATION_FILE = "\"key\",\"en_us\"\n\"hello_world\",\"Hello World!\"\n\"localization_test\",\"This is localization\"";

        private static char LINE_SEPERATOR = '\n';
        private static char ENTRY_SURROUND = '"';
        private static string[] FIELD_SEPERATOR = { "\",\"" };

        private static string FILE_DATA = "";
        private static Dictionary<string, LocalizationLanguage> DATA = new Dictionary<string, LocalizationLanguage>();
        private static bool IS_INIT = false;

        /// <summary>
        /// Creates a CSV file at the give location. <strong>[NOTE] This function should not be in your code when shipping, this is just to set
        /// up localization</strong>
        /// </summary>
        /// <param name="path">The path for the localization file (Excluding '.csv')</param>
        public static void CreateLocalizationFile(string path) {
            var file = File.CreateText(path + ".csv");
            file.Write(DEFAULT_LOCALIZATION_FILE);
            file.Close();
        }

        /// <summary>
        /// Initalizes the <see cref="Localization"/> system
        /// </summary>
        /// <param name="path">Path of the <see cref="Localization"/> file</param>
        /// <param name="langs"><see cref="Languages"/> used in the file</param>
        public static void Init(string path, params Languages[] langs) {
            FILE_DATA = File.ReadAllText(path + ".csv");

            for (int i = 0; i < langs.Length; i++) {
                LocalizationLanguage lang = new LocalizationLanguage();
                lang.SetData(LoadLanguage(langs[i].ToString()));
                DATA.Add(langs[i].ToString(), lang);
            }

            IS_INIT = true;
        }

        /// <summary>
        /// Gets a value from the currently set <see cref="Languages"/>
        /// </summary>
        /// <param name="key">The key of the value in the <see cref="Localization"/> file</param>
        /// <returns>The translated value</returns>
        public static string GetValue(string key) {
            return GetValue(key, CURRENT_LANGUAGE);
        }

        /// <summary>
        /// Gets a value from the currently set <see cref="Languages"/>
        /// </summary>
        /// <param name="key">The key of the value in the <see cref="Localization"/> file</param>
        /// <param name="lang">The <see cref="Languages"/> to translate to</param>
        /// <returns>The translated value</returns>
        public static string GetValue(string key, Languages lang) {
            if(!IS_INIT) { return "LOCALIZATION NOT INITALIZED..."; }

            string value = "NO_DATA";
            LocalizationLanguage data;

            data = DATA[lang.ToString().ToLower()];

            value = data.GetData(key.ToLower());
            return value;
        }

        private static Dictionary<string, string> LoadLanguage(string attributeID) {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] lines = FILE_DATA.Split(LINE_SEPERATOR);
            int attributeIndex = -1;
            string[] headers = lines[0].Split(FIELD_SEPERATOR, StringSplitOptions.None);

            for(int i = 0; i < headers.Length; i++) {
                if (headers[i].ToLower().Contains(attributeID.ToLower())) {
                    attributeIndex = i;
                    break;
                }
            }

            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            for(int i = 1; i < lines.Length; i++) {
                string line = lines[i];
                string[] fields = CSVParser.Split(line);

                for(int f = 0; f < fields.Length; f++) {
                    fields[f] = fields[f].TrimStart(' ', ENTRY_SURROUND);
                    fields[f] = fields[f].TrimEnd(ENTRY_SURROUND);
                }

                if(fields.Length > attributeIndex) {
                    var key = fields[0];
                    if(dictionary.ContainsKey(key)) { continue; }

                    var value = fields[attributeIndex];
                    dictionary.Add(key.ToLower(), value);
                }
            }

            return dictionary;
           
        }

    }

    /// <summary>
    /// A localized language
    /// </summary>
    class LocalizationLanguage {
        private Dictionary<string, string> data = new Dictionary<string, string>();

        public void SetData(Dictionary<string, string> data) {
            this.data = data;
        }

        public string GetData(string key) {
            return data[key];
        }
    }

    /// <summary>
    /// The possible <see cref="Languages"/> supported by <see cref="COLocalization"/>
    /// </summary>
    public enum Languages {
        af,
        sq,
        ar_dz,
        ar_bh,
        ar_eg,
        ar_iq,
        ar_jo,
        ar_kw,
        ar_lb,
        ar_ly,
        ar_ma,
        ar_om,
        ar_qa,
        ar_sa,
        ar_sy,
        ar_tn,
        ar_ae,
        ar_ye,
        eu,
        be,
        bg,
        ca,
        zh_hk,
        zh_cn,
        zh_sg,
        zh_tw,
        hr,
        cs,
        da,
        nl_be,
        nl,
        en,
        en_au,
        en_bz,
        en_ca,
        en_ie,
        en_jm,
        en_nz,
        en_za,
        en_tt,
        en_gb,
        en_us,
        et,
        fo,
        fa,
        fi,
        fr_be,
        fr_ca,
        fr_lu,
        fr,
        fr_ch,
        gd,
        de_at,
        de_li,
        de_lu,
        de,
        de_ch,
        el,
        he,
        hi,
        hu,
        IS,
        id,
        ga,
        it,
        it_ch,
        ja,
        ko,
        ku,
        lv,
        lt,
        mk,
        ml,
        ms,
        mt,
        no,
        nb,
        nn,
        pl,
        pt_br,
        pt,
        pa,
        rm,
        ro,
        ro_md,
        ru,
        ru_md,
        sr,
        sk,
        sl,
        sb,
        es_ar,
        es_bo,
        es_cl,
        es_co,
        es_cr,
        es_do,
        es_ec,
        es_sv,
        es_gt,
        es_hn,
        es_mx,
        es_ni,
        es_pa,
        es_py,
        es_pe,
        es_pr,
        es,
        es_uy,
        es_ve,
        sv,
        sv_fi,
        th,
        ts,
        tn,
        tr,
        ua,
        ur,
        ve,
        vi,
        cy,
        xh,
        ji,
        zu
    }
}