using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Utils.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEZ.LoymarkTechTest.WebAPI.Infrastructure.Persistence.Entities
{
    public partial class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<ChangeType> ChangeTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region relationship definition
            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(x => x.Id).HasName("PK_User");
                entity.Property(x => x.Name)
                            .IsRequired()
                            .HasMaxLength(50);
                entity.Property(x => x.Surname)
                            .IsRequired()
                            .HasMaxLength(50);
                entity.Property(x => x.Birthday)
                            .IsRequired();
                entity.Property(x => x.Email)
                            .IsRequired();
                entity.Property(x => x.Telephone);
                entity.HasIndex(x => x.CountryId, "IX_User_Country");
                entity.HasOne(x => x.Country)
                      .WithMany(x => x.Users)
                      .HasForeignKey(x => x.CountryId)
                      .HasConstraintName("FK_User_Country");
                entity.Property(x => x.WishesToBeContacted)
                            .IsRequired();
                entity.Property(x => x.Active)
                            .IsRequired().HasDefaultValue(true);
            });

            // History
            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");
                entity.HasKey(x => x.Id).HasName("PK_History");
                entity.Property(x => x.ChangeType)
                            .IsRequired();
                entity.Property(x => x.PrevValue)
                            .HasMaxLength(50);
                entity.Property(x => x.CurrValue)
                            .HasMaxLength(50);
                entity.Property(x => x.ChangeDate)
                            .IsRequired()
                            .HasDefaultValue(new DateTime());
                entity.HasIndex(x => x.UserId, "IX_History_User");
                entity.HasOne(x => x.User)
                      .WithMany(x => x.ChangeHistory)
                      .HasForeignKey(x => x.UserId)
                      .HasConstraintName("FK_History_User");
            });

            // Change Type
            modelBuilder.Entity<ChangeType>(entity => {
                entity.ToTable("ChangeType");
                entity.HasKey(x => x.Id);
            });

            // Country
            modelBuilder.Entity<Country>(entity => {
                entity.ToTable("Country");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Code)
                      .HasMaxLength(3);
            });
			#endregion

			#region Seed
			modelBuilder.Entity<ChangeType>().HasData(
				new ChangeType
				{
					Id = (int)ChangeTypeEnum.New,
					Name = "User created"
				},
				new ChangeType
				{
					Id = (int)ChangeTypeEnum.Update,
					Name = "User Updated"
				},
				new ChangeType
				{
					Id = (int)ChangeTypeEnum.Delete,
					Name = "User Deleted"
				}				
			);
			modelBuilder.Entity<Country>().HasData(
				new Country
				{
					Id = (int)CountryEnum.UK,
					Name = "United Kingdom", Code = "GBR", Value = 44
				},
                new Country
				{
					Id = (int)CountryEnum.USA,
					Name = "United States of America", Code = "USA", Value = 1
				},
                new Country
				{
					Id = (int)CountryEnum.CostaRica,
					Name = "Costa Rica", Code = "CRI", Value = 506
				},
                new Country
				{
					Id = (int)CountryEnum.Argentina,
					Name = "Argentina", Code = "ARG", Value = 54
				}
			);

            /*
                <option data-countryCode="GB" value="44" Selected>UK (+44)</option>
			    <option data-countryCode="US" value="1">USA (+1)</option>
			    <optgroup label="Other countries">
				<option data-countryCode="DZ" value="213">Algeria (+213)</option>
				<option data-countryCode="AD" value="376">Andorra (+376)</option>
				<option data-countryCode="AO" value="244">Angola (+244)</option>
				<option data-countryCode="AI" value="1264">Anguilla (+1264)</option>
				<option data-countryCode="AG" value="1268">Antigua &amp; Barbuda (+1268)</option>
				<option data-countryCode="AR" value="54">Argentina (+54)</option>
				<option data-countryCode="AM" value="374">Armenia (+374)</option>
				<option data-countryCode="AW" value="297">Aruba (+297)</option>
				<option data-countryCode="AU" value="61">Australia (+61)</option>
				<option data-countryCode="AT" value="43">Austria (+43)</option>
				<option data-countryCode="AZ" value="994">Azerbaijan (+994)</option>
				<option data-countryCode="BS" value="1242">Bahamas (+1242)</option>
				<option data-countryCode="BH" value="973">Bahrain (+973)</option>
				<option data-countryCode="BD" value="880">Bangladesh (+880)</option>
				<option data-countryCode="BB" value="1246">Barbados (+1246)</option>
				<option data-countryCode="BY" value="375">Belarus (+375)</option>
				<option data-countryCode="BE" value="32">Belgium (+32)</option>
				<option data-countryCode="BZ" value="501">Belize (+501)</option>
				<option data-countryCode="BJ" value="229">Benin (+229)</option>
				<option data-countryCode="BM" value="1441">Bermuda (+1441)</option>
				<option data-countryCode="BT" value="975">Bhutan (+975)</option>
				<option data-countryCode="BO" value="591">Bolivia (+591)</option>
				<option data-countryCode="BA" value="387">Bosnia Herzegovina (+387)</option>
				<option data-countryCode="BW" value="267">Botswana (+267)</option>
				<option data-countryCode="BR" value="55">Brazil (+55)</option>
				<option data-countryCode="BN" value="673">Brunei (+673)</option>
				<option data-countryCode="BG" value="359">Bulgaria (+359)</option>
				<option data-countryCode="BF" value="226">Burkina Faso (+226)</option>
				<option data-countryCode="BI" value="257">Burundi (+257)</option>
				<option data-countryCode="KH" value="855">Cambodia (+855)</option>
				<option data-countryCode="CM" value="237">Cameroon (+237)</option>
				<option data-countryCode="CA" value="1">Canada (+1)</option>
				<option data-countryCode="CV" value="238">Cape Verde Islands (+238)</option>
				<option data-countryCode="KY" value="1345">Cayman Islands (+1345)</option>
				<option data-countryCode="CF" value="236">Central African Republic (+236)</option>
				<option data-countryCode="CL" value="56">Chile (+56)</option>
				<option data-countryCode="CN" value="86">China (+86)</option>
				<option data-countryCode="CO" value="57">Colombia (+57)</option>
				<option data-countryCode="KM" value="269">Comoros (+269)</option>
				<option data-countryCode="CG" value="242">Congo (+242)</option>
				<option data-countryCode="CK" value="682">Cook Islands (+682)</option>
				<option data-countryCode="CR" value="506">Costa Rica (+506)</option>
				<option data-countryCode="HR" value="385">Croatia (+385)</option>
				<option data-countryCode="CU" value="53">Cuba (+53)</option>
				<option data-countryCode="CY" value="90392">Cyprus North (+90392)</option>
				<option data-countryCode="CY" value="357">Cyprus South (+357)</option>
				<option data-countryCode="CZ" value="42">Czech Republic (+42)</option>
				<option data-countryCode="DK" value="45">Denmark (+45)</option>
				<option data-countryCode="DJ" value="253">Djibouti (+253)</option>
				<option data-countryCode="DM" value="1809">Dominica (+1809)</option>
				<option data-countryCode="DO" value="1809">Dominican Republic (+1809)</option>
				<option data-countryCode="EC" value="593">Ecuador (+593)</option>
				<option data-countryCode="EG" value="20">Egypt (+20)</option>
				<option data-countryCode="SV" value="503">El Salvador (+503)</option>
				<option data-countryCode="GQ" value="240">Equatorial Guinea (+240)</option>
				<option data-countryCode="ER" value="291">Eritrea (+291)</option>
				<option data-countryCode="EE" value="372">Estonia (+372)</option>
				<option data-countryCode="ET" value="251">Ethiopia (+251)</option>
				<option data-countryCode="FK" value="500">Falkland Islands (+500)</option>
				<option data-countryCode="FO" value="298">Faroe Islands (+298)</option>
				<option data-countryCode="FJ" value="679">Fiji (+679)</option>
				<option data-countryCode="FI" value="358">Finland (+358)</option>
				<option data-countryCode="FR" value="33">France (+33)</option>
				<option data-countryCode="GF" value="594">French Guiana (+594)</option>
				<option data-countryCode="PF" value="689">French Polynesia (+689)</option>
				<option data-countryCode="GA" value="241">Gabon (+241)</option>
				<option data-countryCode="GM" value="220">Gambia (+220)</option>
				<option data-countryCode="GE" value="7880">Georgia (+7880)</option>
				<option data-countryCode="DE" value="49">Germany (+49)</option>
				<option data-countryCode="GH" value="233">Ghana (+233)</option>
				<option data-countryCode="GI" value="350">Gibraltar (+350)</option>
				<option data-countryCode="GR" value="30">Greece (+30)</option>
				<option data-countryCode="GL" value="299">Greenland (+299)</option>
				<option data-countryCode="GD" value="1473">Grenada (+1473)</option>
				<option data-countryCode="GP" value="590">Guadeloupe (+590)</option>
				<option data-countryCode="GU" value="671">Guam (+671)</option>
				<option data-countryCode="GT" value="502">Guatemala (+502)</option>
				<option data-countryCode="GN" value="224">Guinea (+224)</option>
				<option data-countryCode="GW" value="245">Guinea - Bissau (+245)</option>
				<option data-countryCode="GY" value="592">Guyana (+592)</option>
				<option data-countryCode="HT" value="509">Haiti (+509)</option>
				<option data-countryCode="HN" value="504">Honduras (+504)</option>
				<option data-countryCode="HK" value="852">Hong Kong (+852)</option>
				<option data-countryCode="HU" value="36">Hungary (+36)</option>
				<option data-countryCode="IS" value="354">Iceland (+354)</option>
				<option data-countryCode="IN" value="91">India (+91)</option>
				<option data-countryCode="ID" value="62">Indonesia (+62)</option>
				<option data-countryCode="IR" value="98">Iran (+98)</option>
				<option data-countryCode="IQ" value="964">Iraq (+964)</option>
				<option data-countryCode="IE" value="353">Ireland (+353)</option>
				<option data-countryCode="IL" value="972">Israel (+972)</option>
				<option data-countryCode="IT" value="39">Italy (+39)</option>
				<option data-countryCode="JM" value="1876">Jamaica (+1876)</option>
				<option data-countryCode="JP" value="81">Japan (+81)</option>
				<option data-countryCode="JO" value="962">Jordan (+962)</option>
				<option data-countryCode="KZ" value="7">Kazakhstan (+7)</option>
				<option data-countryCode="KE" value="254">Kenya (+254)</option>
				<option data-countryCode="KI" value="686">Kiribati (+686)</option>
				<option data-countryCode="KP" value="850">Korea North (+850)</option>
				<option data-countryCode="KR" value="82">Korea South (+82)</option>
				<option data-countryCode="KW" value="965">Kuwait (+965)</option>
				<option data-countryCode="KG" value="996">Kyrgyzstan (+996)</option>
				<option data-countryCode="LA" value="856">Laos (+856)</option>
				<option data-countryCode="LV" value="371">Latvia (+371)</option>
				<option data-countryCode="LB" value="961">Lebanon (+961)</option>
				<option data-countryCode="LS" value="266">Lesotho (+266)</option>
				<option data-countryCode="LR" value="231">Liberia (+231)</option>
				<option data-countryCode="LY" value="218">Libya (+218)</option>
				<option data-countryCode="LI" value="417">Liechtenstein (+417)</option>
				<option data-countryCode="LT" value="370">Lithuania (+370)</option>
				<option data-countryCode="LU" value="352">Luxembourg (+352)</option>
				<option data-countryCode="MO" value="853">Macao (+853)</option>
				<option data-countryCode="MK" value="389">Macedonia (+389)</option>
				<option data-countryCode="MG" value="261">Madagascar (+261)</option>
				<option data-countryCode="MW" value="265">Malawi (+265)</option>
				<option data-countryCode="MY" value="60">Malaysia (+60)</option>
				<option data-countryCode="MV" value="960">Maldives (+960)</option>
				<option data-countryCode="ML" value="223">Mali (+223)</option>
				<option data-countryCode="MT" value="356">Malta (+356)</option>
				<option data-countryCode="MH" value="692">Marshall Islands (+692)</option>
				<option data-countryCode="MQ" value="596">Martinique (+596)</option>
				<option data-countryCode="MR" value="222">Mauritania (+222)</option>
				<option data-countryCode="YT" value="269">Mayotte (+269)</option>
				<option data-countryCode="MX" value="52">Mexico (+52)</option>
				<option data-countryCode="FM" value="691">Micronesia (+691)</option>
				<option data-countryCode="MD" value="373">Moldova (+373)</option>
				<option data-countryCode="MC" value="377">Monaco (+377)</option>
				<option data-countryCode="MN" value="976">Mongolia (+976)</option>
				<option data-countryCode="MS" value="1664">Montserrat (+1664)</option>
				<option data-countryCode="MA" value="212">Morocco (+212)</option>
				<option data-countryCode="MZ" value="258">Mozambique (+258)</option>
				<option data-countryCode="MN" value="95">Myanmar (+95)</option>
				<option data-countryCode="NA" value="264">Namibia (+264)</option>
				<option data-countryCode="NR" value="674">Nauru (+674)</option>
				<option data-countryCode="NP" value="977">Nepal (+977)</option>
				<option data-countryCode="NL" value="31">Netherlands (+31)</option>
				<option data-countryCode="NC" value="687">New Caledonia (+687)</option>
				<option data-countryCode="NZ" value="64">New Zealand (+64)</option>
				<option data-countryCode="NI" value="505">Nicaragua (+505)</option>
				<option data-countryCode="NE" value="227">Niger (+227)</option>
				<option data-countryCode="NG" value="234">Nigeria (+234)</option>
				<option data-countryCode="NU" value="683">Niue (+683)</option>
				<option data-countryCode="NF" value="672">Norfolk Islands (+672)</option>
				<option data-countryCode="NP" value="670">Northern Marianas (+670)</option>
				<option data-countryCode="NO" value="47">Norway (+47)</option>
				<option data-countryCode="OM" value="968">Oman (+968)</option>
				<option data-countryCode="PW" value="680">Palau (+680)</option>
				<option data-countryCode="PA" value="507">Panama (+507)</option>
				<option data-countryCode="PG" value="675">Papua New Guinea (+675)</option>
				<option data-countryCode="PY" value="595">Paraguay (+595)</option>
				<option data-countryCode="PE" value="51">Peru (+51)</option>
				<option data-countryCode="PH" value="63">Philippines (+63)</option>
				<option data-countryCode="PL" value="48">Poland (+48)</option>
				<option data-countryCode="PT" value="351">Portugal (+351)</option>
				<option data-countryCode="PR" value="1787">Puerto Rico (+1787)</option>
				<option data-countryCode="QA" value="974">Qatar (+974)</option>
				<option data-countryCode="RE" value="262">Reunion (+262)</option>
				<option data-countryCode="RO" value="40">Romania (+40)</option>
				<option data-countryCode="RU" value="7">Russia (+7)</option>
				<option data-countryCode="RW" value="250">Rwanda (+250)</option>
				<option data-countryCode="SM" value="378">San Marino (+378)</option>
				<option data-countryCode="ST" value="239">Sao Tome &amp; Principe (+239)</option>
				<option data-countryCode="SA" value="966">Saudi Arabia (+966)</option>
				<option data-countryCode="SN" value="221">Senegal (+221)</option>
				<option data-countryCode="CS" value="381">Serbia (+381)</option>
				<option data-countryCode="SC" value="248">Seychelles (+248)</option>
				<option data-countryCode="SL" value="232">Sierra Leone (+232)</option>
				<option data-countryCode="SG" value="65">Singapore (+65)</option>
				<option data-countryCode="SK" value="421">Slovak Republic (+421)</option>
				<option data-countryCode="SI" value="386">Slovenia (+386)</option>
				<option data-countryCode="SB" value="677">Solomon Islands (+677)</option>
				<option data-countryCode="SO" value="252">Somalia (+252)</option>
				<option data-countryCode="ZA" value="27">South Africa (+27)</option>
				<option data-countryCode="ES" value="34">Spain (+34)</option>
				<option data-countryCode="LK" value="94">Sri Lanka (+94)</option>
				<option data-countryCode="SH" value="290">St. Helena (+290)</option>
				<option data-countryCode="KN" value="1869">St. Kitts (+1869)</option>
				<option data-countryCode="SC" value="1758">St. Lucia (+1758)</option>
				<option data-countryCode="SD" value="249">Sudan (+249)</option>
				<option data-countryCode="SR" value="597">Suriname (+597)</option>
				<option data-countryCode="SZ" value="268">Swaziland (+268)</option>
				<option data-countryCode="SE" value="46">Sweden (+46)</option>
				<option data-countryCode="CH" value="41">Switzerland (+41)</option>
				<option data-countryCode="SI" value="963">Syria (+963)</option>
				<option data-countryCode="TW" value="886">Taiwan (+886)</option>
				<option data-countryCode="TJ" value="7">Tajikstan (+7)</option>
				<option data-countryCode="TH" value="66">Thailand (+66)</option>
				<option data-countryCode="TG" value="228">Togo (+228)</option>
				<option data-countryCode="TO" value="676">Tonga (+676)</option>
				<option data-countryCode="TT" value="1868">Trinidad &amp; Tobago (+1868)</option>
				<option data-countryCode="TN" value="216">Tunisia (+216)</option>
				<option data-countryCode="TR" value="90">Turkey (+90)</option>
				<option data-countryCode="TM" value="7">Turkmenistan (+7)</option>
				<option data-countryCode="TM" value="993">Turkmenistan (+993)</option>
				<option data-countryCode="TC" value="1649">Turks &amp; Caicos Islands (+1649)</option>
				<option data-countryCode="TV" value="688">Tuvalu (+688)</option>
				<option data-countryCode="UG" value="256">Uganda (+256)</option>
				<!-- <option data-countryCode="GB" value="44">UK (+44)</option> -->
				<option data-countryCode="UA" value="380">Ukraine (+380)</option>
				<option data-countryCode="AE" value="971">United Arab Emirates (+971)</option>
				<option data-countryCode="UY" value="598">Uruguay (+598)</option>
				<!-- <option data-countryCode="US" value="1">USA (+1)</option> -->
				<option data-countryCode="UZ" value="7">Uzbekistan (+7)</option>
				<option data-countryCode="VU" value="678">Vanuatu (+678)</option>
				<option data-countryCode="VA" value="379">Vatican City (+379)</option>
				<option data-countryCode="VE" value="58">Venezuela (+58)</option>
				<option data-countryCode="VN" value="84">Vietnam (+84)</option>
				<option data-countryCode="VG" value="84">Virgin Islands - British (+1284)</option>
				<option data-countryCode="VI" value="84">Virgin Islands - US (+1340)</option>
				<option data-countryCode="WF" value="681">Wallis &amp; Futuna (+681)</option>
				<option data-countryCode="YE" value="969">Yemen (North)(+969)</option>
				<option data-countryCode="YE" value="967">Yemen (South)(+967)</option>
				<option data-countryCode="ZM" value="260">Zambia (+260)</option>
				<option data-countryCode="ZW" value="263">Zimbabwe (+263)</option>
             */

            /*
			    <option value="AFG">Afghanistan</option>
                <option value="ALA">&Aring;land Islands</option>
                <option value="ALB">Albania</option>
                <option value="DZA">Algeria</option>
                <option value="ASM">American Samoa</option>
                <option value="AND">Andorra</option>
                <option value="AGO">Angola</option>
                <option value="AIA">Anguilla</option>
                <option value="ATA">Antarctica</option>
                <option value="ATG">Antigua and Barbuda</option>
                <option value="ARG">Argentina</option>
                <option value="ARM">Armenia</option>
                <option value="ABW">Aruba</option>
                <option value="AUS">Australia</option>
                <option value="AUT">Austria</option>
                <option value="AZE">Azerbaijan</option>
                <option value="BHS">Bahamas</option>
                <option value="BHR">Bahrain</option>
                <option value="BGD">Bangladesh</option>
                <option value="BRB">Barbados</option>
                <option value="BLR">Belarus</option>
                <option value="BEL">Belgium</option>
                <option value="BLZ">Belize</option>
                <option value="BEN">Benin</option>
                <option value="BMU">Bermuda</option>
                <option value="BTN">Bhutan</option>
                <option value="BOL">Bolivia, Plurinational State of</option>
                <option value="BIH">Bosnia and Herzegovina</option>
                <option value="BWA">Botswana</option>
                <option value="BVT">Bouvet Island</option>
                <option value="BRA">Brazil</option>
                <option value="IOT">British Indian Ocean Territory</option>
                <option value="BRN">Brunei Darussalam</option>
                <option value="BGR">Bulgaria</option>
                <option value="BFA">Burkina Faso</option>
                <option value="BDI">Burundi</option>
                <option value="KHM">Cambodia</option>
                <option value="CMR">Cameroon</option>
                <option value="CAN">Canada</option>
                <option value="CPV">Cape Verde</option>
                <option value="CYM">Cayman Islands</option>
                <option value="CAF">Central African Republic</option>
                <option value="TCD">Chad</option>
                <option value="CHL">Chile</option>
                <option value="CHN">China</option>
                <option value="CXR">Christmas Island</option>
                <option value="CCK">Cocos (Keeling) Islands</option>
                <option value="COL">Colombia</option>
                <option value="COM">Comoros</option>
                <option value="COG">Congo</option>
                <option value="COD">Congo, the Democratic Republic of the</option>
                <option value="COK">Cook Islands</option>
                <option value="CRI">Costa Rica</option>
                <option value="CIV">C&ocirc;te d'Ivoire</option>
                <option value="HRV">Croatia</option>
                <option value="CUB">Cuba</option>
                <option value="CYP">Cyprus</option>
                <option value="CZE">Czech Republic</option>
                <option value="DNK">Denmark</option>
                <option value="DJI">Djibouti</option>
                <option value="DMA">Dominica</option>
                <option value="DOM">Dominican Republic</option>
                <option value="ECU">Ecuador</option>
                <option value="EGY">Egypt</option>
                <option value="SLV">El Salvador</option>
                <option value="GNQ">Equatorial Guinea</option>
                <option value="ERI">Eritrea</option>
                <option value="EST">Estonia</option>
                <option value="ETH">Ethiopia</option>
                <option value="FLK">Falkland Islands (Malvinas)</option>
                <option value="FRO">Faroe Islands</option>
                <option value="FJI">Fiji</option>
                <option value="FIN">Finland</option>
                <option value="FRA">France</option>
                <option value="GUF">French Guiana</option>
                <option value="PYF">French Polynesia</option>
                <option value="ATF">French Southern Territories</option>
                <option value="GAB">Gabon</option>
                <option value="GMB">Gambia</option>
                <option value="GEO">Georgia</option>
                <option value="DEU">Germany</option>
                <option value="GHA">Ghana</option>
                <option value="GIB">Gibraltar</option>
                <option value="GRC">Greece</option>
                <option value="GRL">Greenland</option>
                <option value="GRD">Grenada</option>
                <option value="GLP">Guadeloupe</option>
                <option value="GUM">Guam</option>
                <option value="GTM">Guatemala</option>
                <option value="GGY">Guernsey</option>
                <option value="GIN">Guinea</option>
                <option value="GNB">Guinea-Bissau</option>
                <option value="GUY">Guyana</option>
                <option value="HTI">Haiti</option>
                <option value="HMD">Heard Island and McDonald Islands</option>
                <option value="VAT">Holy See (Vatican City State)</option>
                <option value="HND">Honduras</option>
                <option value="HKG">Hong Kong</option>
                <option value="HUN">Hungary</option>
                <option value="ISL">Iceland</option>
                <option value="IND">India</option>
                <option value="IDN">Indonesia</option>
                <option value="IRN">Iran, Islamic Republic of</option>
                <option value="IRQ">Iraq</option>
                <option value="IRL">Ireland</option>
                <option value="IMN">Isle of Man</option>
                <option value="ISR">Israel</option>
                <option value="ITA">Italy</option>
                <option value="JAM">Jamaica</option>
                <option value="JPN">Japan</option>
                <option value="JEY">Jersey</option>
                <option value="JOR">Jordan</option>
                <option value="KAZ">Kazakhstan</option>
                <option value="KEN">Kenya</option>
                <option value="KIR">Kiribati</option>
                <option value="PRK">Korea, Democratic People's Republic of</option>
                <option value="KOR">Korea, Republic of</option>
                <option value="KWT">Kuwait</option>
                <option value="KGZ">Kyrgyzstan</option>
                <option value="LAO">Lao People's Democratic Republic</option>
                <option value="LVA">Latvia</option>
                <option value="LBN">Lebanon</option>
                <option value="LSO">Lesotho</option>
                <option value="LBR">Liberia</option>
                <option value="LBY">Libyan Arab Jamahiriya</option>
                <option value="LIE">Liechtenstein</option>
                <option value="LTU">Lithuania</option>
                <option value="LUX">Luxembourg</option>
                <option value="MAC">Macao</option>
                <option value="MKD">Macedonia, the former Yugoslav Republic of</option>
                <option value="MDG">Madagascar</option>
                <option value="MWI">Malawi</option>
                <option value="MYS">Malaysia</option>
                <option value="MDV">Maldives</option>
                <option value="MLI">Mali</option>
                <option value="MLT">Malta</option>
                <option value="MHL">Marshall Islands</option>
                <option value="MTQ">Martinique</option>
                <option value="MRT">Mauritania</option>
                <option value="MUS">Mauritius</option>
                <option value="MYT">Mayotte</option>
                <option value="MEX">Mexico</option>
                <option value="FSM">Micronesia, Federated States of</option>
                <option value="MDA">Moldova, Republic of</option>
                <option value="MCO">Monaco</option>
                <option value="MNG">Mongolia</option>
                <option value="MNE">Montenegro</option>
                <option value="MSR">Montserrat</option>
                <option value="MAR">Morocco</option>
                <option value="MOZ">Mozambique</option>
                <option value="MMR">Myanmar</option>
                <option value="NAM">Namibia</option>
                <option value="NRU">Nauru</option>
                <option value="NPL">Nepal</option>
                <option value="NLD">Netherlands</option>
                <option value="ANT">Netherlands Antilles</option>
                <option value="NCL">New Caledonia</option>
                <option value="NZL">New Zealand</option>
                <option value="NIC">Nicaragua</option>
                <option value="NER">Niger</option>
                <option value="NGA">Nigeria</option>
                <option value="NIU">Niue</option>
                <option value="NFK">Norfolk Island</option>
                <option value="MNP">Northern Mariana Islands</option>
                <option value="NOR">Norway</option>
                <option value="OMN">Oman</option>
                <option value="PAK">Pakistan</option>
                <option value="PLW">Palau</option>
                <option value="PSE">Palestinian Territory, Occupied</option>
                <option value="PAN">Panama</option>
                <option value="PNG">Papua New Guinea</option>
                <option value="PRY">Paraguay</option>
                <option value="PER">Peru</option>
                <option value="PHL">Philippines</option>
                <option value="PCN">Pitcairn</option>
                <option value="POL">Poland</option>
                <option value="PRT">Portugal</option>
                <option value="PRI">Puerto Rico</option>
                <option value="QAT">Qatar</option>
                <option value="REU">R&eacute;union</option>
                <option value="ROU">Romania</option>
                <option value="RUS">Russian Federation</option>
                <option value="RWA">Rwanda</option>
                <option value="BLM">Saint Barth&eacute;lemy</option>
                <option value="SHN">Saint Helena, Ascension and Tristan da Cunha</option>
                <option value="KNA">Saint Kitts and Nevis</option>
                <option value="LCA">Saint Lucia</option>
                <option value="MAF">Saint Martin (French part)</option>
                <option value="SPM">Saint Pierre and Miquelon</option>
                <option value="VCT">Saint Vincent and the Grenadines</option>
                <option value="WSM">Samoa</option>
                <option value="SMR">San Marino</option>
                <option value="STP">Sao Tome and Principe</option>
                <option value="SAU">Saudi Arabia</option>
                <option value="SEN">Senegal</option>
                <option value="SRB">Serbia</option>
                <option value="SYC">Seychelles</option>
                <option value="SLE">Sierra Leone</option>
                <option value="SGP">Singapore</option>
                <option value="SVK">Slovakia</option>
                <option value="SVN">Slovenia</option>
                <option value="SLB">Solomon Islands</option>
                <option value="SOM">Somalia</option>
                <option value="ZAF">South Africa</option>
                <option value="SGS">South Georgia and the South Sandwich Islands</option>
                <option value="ESP">Spain</option>
                <option value="LKA">Sri Lanka</option>
                <option value="SDN">Sudan</option>
                <option value="SUR">Suriname</option>
                <option value="SJM">Svalbard and Jan Mayen</option>
                <option value="SWZ">Swaziland</option>
                <option value="SWE">Sweden</option>
                <option value="CHE">Switzerland</option>
                <option value="SYR">Syrian Arab Republic</option>
                <option value="TWN">Taiwan, Province of China</option>
                <option value="TJK">Tajikistan</option>
                <option value="TZA">Tanzania, United Republic of</option>
                <option value="THA">Thailand</option>
                <option value="TLS">Timor-Leste</option>
                <option value="TGO">Togo</option>
                <option value="TKL">Tokelau</option>
                <option value="TON">Tonga</option>
                <option value="TTO">Trinidad and Tobago</option>
                <option value="TUN">Tunisia</option>
                <option value="TUR">Turkey</option>
                <option value="TKM">Turkmenistan</option>
                <option value="TCA">Turks and Caicos Islands</option>
                <option value="TUV">Tuvalu</option>
                <option value="UGA">Uganda</option>
                <option value="UKR">Ukraine</option>
                <option value="ARE">United Arab Emirates</option>
                <option value="GBR">United Kingdom</option>
                <option value="USA">United States</option>
                <option value="UMI">United States Minor Outlying Islands</option>
                <option value="URY">Uruguay</option>
                <option value="UZB">Uzbekistan</option>
                <option value="VUT">Vanuatu</option>
                <option value="VEN">Venezuela, Bolivarian Republic of</option>
                <option value="VNM">Viet Nam</option>
                <option value="VGB">Virgin Islands, British</option>
                <option value="VIR">Virgin Islands, U.S.</option>
                <option value="WLF">Wallis and Futuna</option>
                <option value="ESH">Western Sahara</option>
                <option value="YEM">Yemen</option>
                <option value="ZMB">Zambia</option>
                <option value="ZWE">Zimbabwe</option>
			 */

            #endregion
            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
