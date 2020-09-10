using System;
using System.Collections.Generic;
using System.Globalization;

namespace OneBitOfEngine
{
    /// <summary>
    /// A "toolbox" static classes providing various helper methods.
    /// </summary>
    public static class OneBitOfTools
    {
        /// <summary>
        /// Two times Pi.
        /// </summary>
        public const float TWO_PI = 6.28318530718f;

        /// <summary>
        /// The square root of two.
        /// </summary>
        public const float SQUARE_ROOT_OF_TWO = 1.41421356237f;

        /// <summary>
        /// (Private) Random number generators. Index #0 is non-seeded, Index #1 is seeded.
        /// </summary>
        private static readonly Random[] RNG = new Random[] { new Random(), new Random(1) };

        /// <summary>
        /// An array containing the value of cos for each of 360 degrees.
        /// </summary>
        private static readonly float[] COS_TABLE = { 1.0f, 0.99984769502585f, 0.999390826497f, 0.99862953358015f, 0.99756404817269f, 0.9961946948321f, 0.994521890677f, 0.99254614526019f, 0.99026806041339f, 0.98768833006389f, 0.98480774002322f, 0.98162716774778f, 0.97814758207151f, 0.97437004291082f, 0.97029570094174f, 0.96592579724935f, 0.96126166294978f, 0.95630471878476f, 0.95105647468878f, 0.94551852932921f, 0.9396925696193f, 0.93358037020433f, 0.92718379292105f, 0.92050478623053f, 0.91354538462467f, 0.90630770800642f, 0.89879396104408f, 0.89100643249973f, 0.88294749453204f, 0.87461960197368f, 0.86602529158357f, 0.85716718127416f, 0.84804796931396f, 0.83867043350568f, 0.82903743034f, 0.81915189412551f, 0.80901683609491f, 0.79863534348767f, 0.78801057860975f, 0.7771457778702f, 0.76604425079543f, 0.75470937902103f, 0.74314461526169f, 0.73135348225952f, 0.71933957171092f, 0.70710654317256f, 0.69465812294664f, 0.6819981029458f, 0.66913033953807f, 0.65605875237221f, 0.6427873231837f, 0.6293200945819f, 0.61566116881863f, 0.60181470653854f, 0.5877849255118f, 0.57357609934929f, 0.55919255620081f, 0.54463867743668f, 0.52991889631318f, 0.51503769662206f, 0.4999996113248f, 0.48480922117178f, 0.46947115330696f, 0.4539900798584f, 0.43837071651508f, 0.42261782109046f, 0.40673619207321f, 0.39073066716552f, 0.37460612180949f, 0.35836746770207f, 0.34201965129884f, 0.3255676523073f, 0.30901648217002f, 0.29237118253809f, 0.27563682373535f, 0.25881850321398f, 0.24192134400173f, 0.22495049314139f, 0.20791112012296f, 0.19080841530898f, 0.17364758835347f, 0.15643386661505f, 0.13917249356461f, 0.1218687271881f, 0.1045278383849f, 0.08715510936225f, 0.069755832026244f, 0.05233530636985f, 0.034898838858488f, 0.017451740813626f, -6.7320510360158e-07f, -0.017453087018768f, -0.034900184448498f, -0.052336650934847f, -0.069757175156661f, -0.087156450648955f, -0.10452917741932f, -0.12187006356236f, -0.13917382687164f, -0.1564351964487f, -0.17364891430867f, -0.19080973698182f, -0.20791243711085f, -0.22495180504316f, -0.24192265041777f, -0.25881980374634f, -0.27563811798786f, -0.29237247011652f, -0.30901776268217f, -0.3255689253631f, -0.3420209165105f, -0.35836872468421f, -0.37460737017922f, -0.39073190654255f, -0.40673742208004f, -0.42261904135241f, -0.43837192666044f, -0.45399127951855f, -0.46947234211648f, -0.48481039876854f, -0.50000077735009f, -0.51503885072071f, -0.52992003813362f, -0.54463980663112f, -0.55919367242526f, -0.57357720226376f, -0.58778601478033f, -0.60181578182932f, -0.61566222980411f, -0.62932114093891f, -0.6427883545935f, -0.65605976852062f, -0.66913134011557f, -0.68199908764759f, -0.69465909147278f, -0.70710749522803f, -0.7193405070057f, -0.73135440050872f, -0.74314551618561f, -0.75471026234523f, -0.76604511625085f, -0.7771466251932f, -0.78801140754223f, -0.79863615377714f, -0.80901762749453f, -0.81915266639423f, -0.82903818324256f, -0.83867116681275f, -0.84804868280218f, -0.85716787472617f, -0.86602596478815f, -0.87462025472576f, -0.88294812663279f, -0.89100704375661f, -0.89879455127089f, -0.90630827702337f, -0.91354593225843f, -0.92050531231429f, -0.92718429729455f, -0.93358085271394f, -0.93969303011805f, -0.94551896767682f, -0.95105689075173f, -0.9563051124363f, -0.96126203407002f, -0.96592614572522f, -0.97029602666711f, -0.97437034578646f, -0.97814786200516f, -0.98162742465418f, -0.98480797382411f, -0.98768854068804f, -0.99026824779665f, -0.99254630934549f, -0.99452203141435f, -0.99619481217862f, -0.99756414209266f, -0.99862960404494f, -0.99939087348515f, -0.99984771852305f, -0.99999999999909f, -0.99984767152683f, -0.99939077950703f, -0.99862946311354f, -0.99756395425092f, -0.99619457748376f, -0.99452174993785f, -0.9925459811731f, -0.99026787302833f, -0.98768811943794f, -0.98480750622055f, -0.9816269108396f, -0.97814730213608f, -0.97436974003342f, -0.97029537521461f, -0.96592544877172f, -0.96126129182781f, -0.95630432513148f, -0.95105605862411f, -0.94551809097989f, -0.93969210911884f, -0.93357988769302f, -0.92718328854586f, -0.92050426014511f, -0.91354483698925f, -0.90630713898783f, -0.89879337081565f, -0.89100582124124f, -0.88294686242968f, -0.87461894922001f, -0.86602461837742f, -0.85716648782059f, -0.84804725582422f, -0.83866970019709f, -0.82903667743593f, -0.81915112185531f, -0.80901604469382f, -0.79863453319676f, -0.78800974967584f, -0.77714493054579f, -0.76604338533863f, -0.75470849569546f, -0.74314371433643f, -0.73135256400898f, -0.71933863641482f, -0.70710559111581f, -0.69465715441924f, -0.68199711824277f, -0.66912933895937f, -0.65605773622261f, -0.64278629177274f, -0.62931904822375f, -0.61566010783202f, -0.60181363124667f, -0.58778383624221f, -0.57357499643378f, -0.55919143997533f, -0.54463754824126f, -0.52991775449178f, -0.51503654252249f, -0.4999984452986f, -0.48480804357414f, -0.46946996449659f, -0.45398888019742f, -0.43836950636892f, -0.42261660082775f, -0.40673496206564f, -0.39072942778777f, -0.37460487343909f, -0.35836621071928f, -0.34201838608655f, -0.32556637925091f, -0.30901520165732f, -0.29236989495912f, -0.27563552948234f, -0.25881720268116f, -0.24192003758526f, -0.22494918123921f, -0.2079098031347f, -0.19080709363579f, -0.17364626239796f, -0.15643253678112f, -0.13917116025734f, -0.12186739081362f, -0.10452649935028f, -0.087153768075387f, -0.0697544888957f, -0.052333961804758f, -0.034897493268416f, -0.017450394608452f, 2.0196153103594e-06f, 0.017454433223879f, 0.034901530038444f, 0.052337995499749f, 0.069758518286951f, 0.087157791935502f, 0.10453051645356f, 0.1218713999364f, 0.13917516017841f, 0.15643652628207f, 0.17365024026354f, 0.19081105865431f, 0.20791375409836f, 0.22495311694453f, 0.24192395683336f, 0.25882110427822f, 0.27563941223988f, 0.29237375769442f, 0.30901904319375f, 0.32557019841831f, 0.34202218172155f, 0.3583699816657f, 0.37460861854826f, 0.39073314591889f, 0.40673865208613f, 0.42262026161359f, 0.43837313680501f, 0.45399247917789f, 0.46947353092515f, 0.48481157636442f, 0.50000194337448f, 0.51504000481841f, 0.5299211799531f, 0.54464093582456f, 0.55919478864871f, 0.57357830517719f, 0.58778710404779f, 0.601816857119f, 0.61566329078848f, 0.62932218729478f, 0.64278938600213f, 0.65606078466784f, 0.66913234069185f, 0.68200007234815f, 0.69466005999767f, 0.70710844728221f, 0.71934144229919f, 0.73135531875661f, 0.74314641710818f, 0.75471114566806f, 0.76604598170487f, 0.77714747251479f, 0.78801223647328f, 0.79863696406515f, 0.80901841889269f, 0.81915343866146f, 0.82903893614363f, 0.83867190011831f, 0.84804939628885f, 0.85716856817662f, 0.86602663799116f, 0.87462090747626f, 0.88294875873194f, 0.89100765501187f, 0.89879514149607f, 0.90630884603867f, 0.91354647989054f, 0.92050583839638f, 0.92718480166638f, 0.93358133522187f, 0.93969349061509f, 0.94551940602272f, 0.95105730681295f, 0.95630550608612f, 0.96126240518851f, 0.96592649419934f, 0.97029635239071f, 0.97437064866034f, 0.97814814193704f, 0.98162768155879f, 0.98480820762321f, 0.98768875131041f, 0.99026843517812f, 0.99254647342899f, 0.99452217214989f, 0.99619492952335f, 0.99756423601081f, 0.99862967450792f, 0.99939092047149f, 0.99984774201844f };

        /// <summary>
        /// An array containing the value of sin for each of 360 degrees.
        /// </summary>
        private static readonly float[] SIN_TABLE = { 0.0f, 0.017452413916201f, 0.034899511653501f, 0.05233597865236f, 0.069756503591468f, 0.087155780005622f, 0.10452850790213f, 0.12186939537526f, 0.13917316021816f, 0.15643453153191f, 0.17364825133111f, 0.19080907614544f, 0.20791177861695f, 0.22495114909233f, 0.24192199720981f, 0.25881915348022f, 0.27563747086167f, 0.29237182632737f, 0.30901712242616f, 0.32556828883527f, 0.34202028390475f, 0.35836809619322f, 0.37460674599444f, 0.39073128685412f, 0.40673680707672f, 0.42261843122153f, 0.43837132158786f, 0.45399067968858f, 0.46947174771183f, 0.48480980997027f, 0.50000019433756f, 0.5150382736715f, 0.52991946722352f, 0.54463924203402f, 0.55919311431316f, 0.57357665080666f, 0.5877854701462f, 0.60181524418406f, 0.61566169931151f, 0.62932061776055f, 0.64278783888875f, 0.65605926044657f, 0.66913083982697f, 0.68199859529685f, 0.69465860720987f, 0.70710701920045f, 0.71934003935847f, 0.73135394138429f, 0.74314506572382f, 0.7547098206833f, 0.76604468352331f, 0.77714620153188f, 0.78801099307617f, 0.79863574863259f, 0.8090172317949f, 0.81915228026006f, 0.82903780679147f, 0.83867080015941f, 0.84804832605826f, 0.85716752800036f, 0.86602562818605f, 0.87461992834992f, 0.88294781058261f, 0.89100673812837f, 0.89879425615769f, 0.9063079925151f, 0.91354565844176f, 0.92050504927262f, 0.92718404510801f, 0.93358061145935f, 0.93969279986888f, 0.94551874850323f, 0.95105668272047f, 0.95630491561075f, 0.96126184851012f, 0.9659259714875f, 0.97029586380464f, 0.97437019434887f, 0.97814772203856f, 0.9816272962012f, 0.98480785692389f, 0.98768843537619f, 0.99026815410524f, 0.99254622730307f, 0.9945219610459f, 0.99619475350559f, 0.9975640951329f, 0.99862956881277f, 0.9993908499913f, 0.99984770677467f, 0.99999999999977f, 0.99984768327657f, 0.99939080300224f, 0.99862949834707f, 0.99756400121203f, 0.99619463615815f, 0.99452182030765f, 0.99254606321687f, 0.99026796672108f, 0.98768822475114f, 0.98480762312211f, 0.98162703929391f, 0.97814744210401f, 0.97436989147234f, 0.9702955380784f, 0.96592562301075f, 0.96126147738901f, 0.95630452195834f, 0.95105626665666f, 0.94551831015476f, 0.93969233936928f, 0.93358012894888f, 0.92718354073366f, 0.92050452318803f, 0.91354511080717f, 0.90630742349733f, 0.89879366593007f, 0.89100612687069f, 0.88294717848106f, 0.87461927559704f, 0.86602495498069f, 0.85716683454757f, 0.84804761256928f, 0.83867006685157f, 0.82903705388815f, 0.8191515079906f, 0.80901644039454f, 0.7986349383424f, 0.78801016414297f, 0.77714535420817f, 0.76604381806721f, 0.75470893735841f, 0.74314416479923f, 0.73135302313442f, 0.71933910406303f, 0.70710606714435f, 0.6946576386831f, 0.68199761059444f, 0.66912983924887f, 0.65605824429756f, 0.64278680747836f, 0.62931957140297f, 0.61566063832546f, 0.60181416889274f, 0.58778438087714f, 0.57357554789167f, 0.5591919980882f, 0.5446381128391f, 0.5299183254026f, 0.51503711957239f, 0.49999902831182f, 0.48480863237307f, 0.46947055890188f, 0.45398948002801f, 0.4383701114421f, 0.4226172109592f, 0.40673557706952f, 0.39073004747673f, 0.37460549762438f, 0.35836683921076f, 0.34201901869277f, 0.32556701577918f, 0.30901584191374f, 0.29237053874867f, 0.2756361766089f, 0.25881785294763f, 0.24192069079355f, 0.22494983719035f, 0.20791046162888f, 0.19080775447243f, 0.17364692537576f, 0.15643320169812f, 0.13917182691101f, 0.12186805900089f, 0.10452716886761f, 0.087154438718838f, 0.069755160460988f, 0.052334634087316f, 0.03489816606346f, 0.017451067711043f, -1.3464102072029e-06f, -0.017453760121327f, -0.034900857243479f, -0.05233732321731f, -0.069757846721822f, -0.087157121292248f, -0.10452984693646f, -0.12187073174941f, -0.13917449352505f, -0.15643586136542f, -0.17364957728614f, -0.19081039781811f, -0.20791309560465f, -0.22495246099389f, -0.24192330362562f, -0.25882045401234f, -0.27563876511393f, -0.29237311390554f, -0.30901840293803f, -0.32556956189078f, -0.3420215491161f, -0.35836935317504f, -0.37460799436382f, -0.39073252623081f, -0.40673803708318f, -0.4226196514831f, -0.43837253173283f, -0.45399187934832f, -0.46947293652092f, -0.48481098756659f, -0.5000013603624f, -0.51503942776968f, -0.52992060904348f, -0.54464037122796f, -0.55919423053711f, -0.57357775372061f, -0.58778655941419f, -0.6018163194743f, -0.61566276029644f, -0.62932166411699f, -0.64278887029796f, -0.65606027659438f, -0.66913184040386f, -0.68199957999803f, -0.69465957573538f, -0.70710797125528f, -0.71934097465261f, -0.73135485963283f, -0.74314596664707f, -0.75471070400682f, -0.76604554897803f, -0.77714704885417f, -0.78801182200793f, -0.79863655892133f, -0.80901802319379f, -0.81915305252803f, -0.82903855969328f, -0.83867153346572f, -0.8480490395457f, -0.85716822145159f, -0.86602630138985f, -0.87462058110121f, -0.88294844268257f, -0.89100734938444f, -0.89879484638368f, -0.90630856153123f, -0.91354620607469f, -0.92050557535555f, -0.92718454948068f, -0.93358109396812f, -0.93969326036678f, -0.94551918684998f, -0.95105709878255f, -0.95630530926143f, -0.96126221962948f, -0.9659263199625f, -0.97029618952913f, -0.97437049722362f, -0.97814800197132f, -0.98162755310671f, -0.98480809072388f, -0.98768864599945f, -0.99026834148761f, -0.99254639138747f, -0.99452210178235f, -0.99619487085121f, -0.99756418905196f, -0.99862963927665f, -0.99939089697855f, -0.99984773027097f, -0.99999999999796f, -0.99984765977664f, -0.99939075601137f, -0.99862942787957f, -0.99756390728936f, -0.99619451880892f, -0.9945216795676f, -0.99254589912887f, -0.99026777933512f, -0.9876880141243f, -0.98480738931855f, -0.98162678238484f, -0.9781471621677f, -0.97436958859405f, -0.97029521235039f, -0.96592527453225f, -0.96126110626617f, -0.95630412830419f, -0.95105585059113f, -0.94551787180458f, -0.93969187886798f, -0.93357964643673f, -0.92718303635764f, -0.92050399710177f, -0.91354456317092f, -0.90630685447792f, -0.89879307570082f, -0.89100551561139f, -0.88294654637791f, -0.87461862284258f, -0.86602428177375f, -0.85716614109322f, -0.84804689907877f, -0.83866933354222f, -0.82903630098333f, -0.81915073571966f, -0.80901564899272f, -0.79863412805076f, -0.78800933520834f, -0.77714450688306f, -0.76604295260971f, -0.75470805403216f, -0.74314326387329f, -0.73135210488322f, -0.71933816876629f, -0.70710511508696f, -0.69465667015507f, -0.68199662589079f, -0.66912883866956f, -0.65605722814736f, -0.64278577606682f, -0.62931852504425f, -0.6156595773383f, -0.60181309360033f, -0.58778329160702f, -0.57357444497563f, -0.55919088186222f, -0.54463698364318f, -0.52991718358072f, -0.51503596547235f, -0.49999786228516f, -0.48480745477499f, -0.46946937009109f, -0.45398828036662f, -0.43836890129554f, -0.4226159906961f, -0.40673434706158f, -0.39072880809863f, -0.37460424925364f, -0.35836558222764f, -0.34201775348017f, -0.32556574272249f, -0.30901456140075f, -0.29236925116944f, -0.27563488235564f, -0.25881655241457f, -0.24191938437686f, -0.22494852528797f, -0.20790914464042f, -0.19080643279907f, -0.17364559942009f, -0.15643187186405f, -0.1391704936036f, -0.12186672262629f, -0.10452582983291f, -0.087153097431895f, -0.069753817330382f, -0.052333289522177f, -0.034896820473355f, -0.017449721505854f };

        /// <summary>
        /// Sets the seed used by the random number generator when OneBitOfTools.RandomXXX() methods are used with the "seeded = true" parameter.
        /// Changing this will reinitialize the RNG.
        /// </summary>
        /// <param name="seed">The seed</param>
        public static void SetRandomSeed(int seed)
        {
            RNG[1] = new Random(Math.Max(0, seed));
        }

        /// <summary>
        /// Clamps the value between minValue and maxValue.
        /// </summary>
        /// <param name="value">A value</param>
        /// <param name="minValue">Minimum acceptable value</param>
        /// <param name="maxValue">Maximum acceptable value</param>
        /// <returns>The clamped value</returns>
        public static int Clamp(int value, int minValue, int maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        /// <summary>
        /// Clamps the value between minValue and maxValue.
        /// </summary>
        /// <param name="value">A value</param>
        /// <param name="minValue">Minimum acceptable value</param>
        /// <param name="maxValue">Maximum acceptable value</param>
        /// <returns>The clamped value</returns>
        public static float Clamp(float value, float minValue, float maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        /// <summary>
        /// Clamps the value between minValue and maxValue.
        /// </summary>
        /// <param name="value">A value</param>
        /// <param name="minValue">Minimum acceptable value</param>
        /// <param name="maxValue">Maximum acceptable value</param>
        /// <returns>The clamped value</returns>
        public static double Clamp(double value, double minValue, double maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        /// <summary>
        /// Returns a random boolean.
        /// </summary>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A boolean</returns>
        public static bool RandomBool(bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].NextDouble() < .5f;
        }

        /// <summary>
        /// Returns a random float between 0 and 1 (both inclusive).
        /// </summary>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A float</returns>
        public static float RandomFloat(bool useSeed = false)
        {
            return (float)RandomDouble(useSeed);
        }

        /// <summary>
        /// Returns a random float between 0 and maxValue (both inclusive).
        /// </summary>
        /// <param name="maxValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A float</returns>
        public static float RandomFloat(float maxValue, bool useSeed = false)
        {
            return (float)RandomDouble(0.0, maxValue, useSeed);
        }

        /// <summary>
        /// Returns a random float between minValue and maxValue (both inclusive).
        /// </summary>
        /// <param name="minValue">The minimum possible value, INCLUSIVE</param>
        /// <param name="maxValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A float</returns>
        public static float RandomFloat(float minValue, float maxValue, bool useSeed = false)
        {
            return (float)RandomDouble(minValue, maxValue, useSeed);
        }

        /// <summary>
        /// Returns a random double between 0 and 1 (both inclusive).
        /// </summary>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A double</returns>
        public static double RandomDouble(bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].NextDouble();
        }

        /// <summary>
        /// Returns a random double between 0 and maxValue (both inclusive).
        /// </summary>
        /// <param name="maxValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A double</returns>
        public static double RandomDouble(double maxValue, bool useSeed = false)
        {
            return RandomDouble(0.0, maxValue, useSeed);
        }

        /// <summary>
        /// Returns a random double between minValue and maxValue (both inclusive).
        /// </summary>
        /// <param name="minValue">The minimum possible value, INCLUSIVE</param>
        /// <param name="maxValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A double</returns>
        public static double RandomDouble(double minValue, double maxValue, bool useSeed = false)
        {
            double value = RandomDouble(useSeed);

            return value * (Math.Max(minValue, maxValue) - Math.Min(minValue, maxValue)) + Math.Min(minValue, maxValue);
        }

        /// <summary>
        /// Returns a random integer between 0 (inclusive) and <see cref="int.MaxValue"/> (exclusive).
        /// </summary>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>An integer</returns>
        public static int RandomInt(bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].Next();
        }

        /// <summary>
        /// Returns a random integer between 0 (inclusive) and maxValue (exclusive).
        /// </summary>
        /// <param name="maxValue">The maximum possible value, EXCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>An integer</returns>
        public static int RandomInt(int maxValue, bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].Next(maxValue);
        }

        /// <summary>
        /// Returns a random integer between minValue (inclusive) and maxValue (exclusive).
        /// </summary>
        /// <param name="minValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="maxValue">The maximum possible value, EXCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>An integer</returns>
        public static int RandomInt(int minValue, int maxValue, bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].Next(minValue, maxValue);
        }

        /// <summary>
        /// Returns a random element from a array.
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="array">The array</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>An element from the array</returns>
        public static T RandomFromArray<T>(T[] array, bool useSeed = false)
        {
            if ((array == null) || (array.Length == 0)) return default;
            return array[RNG[useSeed ? 1 : 0].Next(array.Length)];
        }

        /// <summary>
        /// Returns a random element from a list.
        /// </summary>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <param name="list">The list</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>An element from the list</returns>
        public static T RandomFromList<T>(List<T> list, bool useSeed = false)
        {
            if ((list == null) || (list.Count == 0)) return default;
            return list[RNG[useSeed ? 1 : 0].Next(list.Count)];
        }

        /// <summary>
        /// Returns the number of values in an enumeration of type T.
        /// </summary>
        /// <typeparam name="T">A type of enumeration</typeparam>
        /// <returns>The number of values</returns>
        public static int EnumCount<T>() where T: Enum
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        /// <summary>
        /// Splits a string into multiples lines of maximum length lineLength.
        /// </summary>
        /// <param name="text">The text to split in lines</param>
        /// <param name="lineLength">Max length of each line</param>
        /// <returns>An array of lines</returns>
        public static string[] WordWrap(string text, int lineLength)
        {
            if (string.IsNullOrEmpty(text)) return new string[0];
            lineLength = Math.Max(1, lineLength);

            string[] words = text.Replace("\\n", "\n").Replace("\n", " \n ").Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> lines = new List<string>();
            string currentLine = "";

            foreach (string w in words)
            {
                if (w == "\n")
                {
                    lines.Add(currentLine);
                    currentLine = "";
                    continue;
                }

                if (currentLine.Length + w.Length >= lineLength)
                {
                    lines.Add(currentLine);
                    currentLine = "";
                }

                if (currentLine.Length > 0) currentLine += " ";
                currentLine += w;
            }

            // Add the last line
            if (currentLine.Length > 0) lines.Add(currentLine);

            return lines.ToArray();
        }

        /// <summary>
        /// Returns an interpolation between value1 and value2.
        /// </summary>
        /// <param name="value1">The first value</param>
        /// <param name="value2">The second value</param>
        /// <param name="interpolation">The position between the first and the second value, between 0.0 and 1.0</param>
        /// <returns>The interpolated value</returns>
        public static float Lerp(float value1, float value2, float interpolation)
        {
            interpolation = Clamp(interpolation, 0f, 1f);
            return value1 * (1f - interpolation) + value2 * interpolation;
        }

        /// <summary>
        /// Returns the next power of two, or the input value if already was a power of two.
        /// </summary>
        /// <param name="value">An integer</param>
        /// <returns>An integer</returns>
        public static int RoundUpToNextPowerOfTwo(int value)
        {
            int p = 1;
            while (p < value) p <<= 1;
            return p;
        }

        /// <summary>
        /// Returns a fast approximation of the cosine of an angle in degrees
        /// </summary>
        /// <param name="angleInDegrees">The angle, in degrees</param>
        /// <returns>The cosine</returns>
        public static float FastCos(int angleInDegrees)
        {
            while (angleInDegrees < 0) { angleInDegrees += 360; }
            return COS_TABLE[angleInDegrees % 360];
        }

        /// <summary>
        /// Returns a fast approximation of the cosine of an angle in degrees
        /// </summary>
        /// <param name="angleInDegrees">The angle, in degrees</param>
        /// <returns>The cosine</returns>
        public static float FastCos(float angleInDegrees)
        {
            float vFloor = FastCos((int)Math.Floor(angleInDegrees));
            float vCeiling = FastCos((int)Math.Ceiling(angleInDegrees));
            float interpolation = angleInDegrees - (float)Math.Floor(angleInDegrees);

            return Lerp(vFloor, vCeiling, interpolation);
        }

        /// <summary>
        /// Returns a fast approximation of the sine of an angle in degrees
        /// </summary>
        /// <param name="angleInDegrees">The angle, in degrees</param>
        /// <returns>The sine</returns>
        public static float FastSin(int angleInDegrees)
        {
            while (angleInDegrees < 0) { angleInDegrees += 360; }
            return SIN_TABLE[angleInDegrees % 360];
        }

        /// <summary>
        /// Returns a fast approximation of the sine of an angle in degrees
        /// </summary>
        /// <param name="angleInDegrees">The angle, in degrees</param>
        /// <returns>The sine</returns>
        public static float FastSin(float angleInDegrees)
        {
            float vFloor = FastSin((int)Math.Floor(angleInDegrees));
            float vCeiling = FastSin((int)Math.Ceiling(angleInDegrees));
            float interpolation = angleInDegrees - (float)Math.Floor(angleInDegrees);

            return Lerp(vFloor, vCeiling, interpolation);
        }

        /// <summary>
        /// Returns a string with the first character uppercased.
        /// </summary>
        /// <param name="str">A string</param>
        /// <returns>A string</returns>
        public static string ICase(string str, CultureInfo culture = null)
        {
            if (string.IsNullOrEmpty(str)) return str;

            if (str.Length == 1) return str.ToUpper(culture ?? CultureInfo.InvariantCulture);

            return str.Substring(0, 1).ToUpper(culture ?? CultureInfo.InvariantCulture) + str.Substring(1);
        }
    }
}
