class Program
{
    //define the periodic table, key being the symbol and value being the element (thanks ChatGPT)
    static readonly Dictionary<string, string> periodicTable = new()
    {
        { "H", "Hydrogen" }, { "He", "Helium" }, { "Li", "Lithium" }, { "Be", "Beryllium" },
        { "B", "Boron" }, { "C", "Carbon" }, { "N", "Nitrogen" }, { "O", "Oxygen" },
        { "F", "Fluorine" }, { "Ne", "Neon" }, { "Na", "Sodium" }, { "Mg", "Magnesium" },
        { "Al", "Aluminium" }, { "Si", "Silicon" }, { "P", "Phosphorus" }, { "S", "Sulfur" },
        { "Cl", "Chlorine" }, { "Ar", "Argon" }, { "K", "Potassium" }, { "Ca", "Calcium" },
        { "Sc", "Scandium" }, { "Ti", "Titanium" }, { "V", "Vanadium" }, { "Cr", "Chromium" },
        { "Mn", "Manganese" }, { "Fe", "Iron" }, { "Co", "Cobalt" }, { "Ni", "Nickel" },
        { "Cu", "Copper" }, { "Zn", "Zinc" }, { "Ga", "Gallium" }, { "Ge", "Germanium" },
        { "As", "Arsenic" }, { "Se", "Selenium" }, { "Br", "Bromine" }, { "Kr", "Krypton" },
        { "Rb", "Rubidium" }, { "Sr", "Strontium" }, { "Y", "Yttrium" }, { "Zr", "Zirconium" },
        { "Nb", "Niobium" }, { "Mo", "Molybdenum" }, { "Tc", "Technetium" }, { "Ru", "Ruthenium" },
        { "Rh", "Rhodium" }, { "Pd", "Palladium" }, { "Ag", "Silver" }, { "Cd", "Cadmium" },
        { "In", "Indium" }, { "Sn", "Tin" }, { "Sb", "Antimony" }, { "Te", "Tellurium" },
        { "I", "Iodine" }, { "Xe", "Xenon" }, { "Cs", "Cesium" }, { "Ba", "Barium" },
        { "La", "Lanthanum" }, { "Ce", "Cerium" }, { "Pr", "Praseodymium" }, { "Nd", "Neodymium" },
        { "Pm", "Promethium" }, { "Sm", "Samarium" }, { "Eu", "Europium" }, { "Gd", "Gadolinium" },
        { "Tb", "Terbium" }, { "Dy", "Dysprosium" }, { "Ho", "Holmium" }, { "Er", "Erbium" },
        { "Tm", "Thulium" }, { "Yb", "Ytterbium" }, { "Lu", "Lutetium" }, { "Hf", "Hafnium" },
        { "Ta", "Tantalum" }, { "W", "Tungsten" }, { "Re", "Rhenium" }, { "Os", "Osmium" },
        { "Ir", "Iridium" }, { "Pt", "Platinum" }, { "Au", "Gold" }, { "Hg", "Mercury" },
        { "Tl", "Thallium" }, { "Pb", "Lead" }, { "Bi", "Bismuth" }, { "Po", "Polonium" },
        { "At", "Astatine" }, { "Rn", "Radon" }, { "Fr", "Francium" }, { "Ra", "Radium" },
        { "Ac", "Actinium" }, { "Th", "Thorium" }, { "Pa", "Protactinium" }, { "U", "Uranium" },
        { "Np", "Neptunium" }, { "Pu", "Plutonium" }, { "Am", "Americium" }, { "Cm", "Curium" },
        { "Bk", "Berkelium" }, { "Cf", "Californium" }, { "Es", "Einsteinium" }, { "Fm", "Fermium" },
        { "Md", "Mendelevium" }, { "No", "Nobelium" }, { "Lr", "Lawrencium" }, { "Rf", "Rutherfordium" },
        { "Db", "Dubnium" }, { "Sg", "Seaborgium" }, { "Bh", "Bohrium" }, { "Hs", "Hassium" },
        { "Mt", "Meitnerium" }, { "Ds", "Darmstadtium" }, { "Rg", "Roentgenium" }, { "Cn", "Copernicium" },
        { "Nh", "Nihonium" }, { "Fl", "Flerovium" }, { "Mc", "Moscovium" }, { "Lv", "Livermorium" },
        { "Ts", "Tennessine" }, { "Og", "Oganesson" }
    };
    static void Main()
    {
        while (true)
        {
            //allows for user input to decide on word
            string word = Console.ReadLine()!;
            foreach (var element in elementalForms(word))
            {
                //make it a bit tidy
                Console.WriteLine(String.Join(", ", element));
            }
        }
    }
    //function that takes one parameter
    public static string[][] elementalForms(string word)
    {
        var result = new List<string[]>();
        //call function that will deal with the element lookup taking into account capitalisation not mattering
        LookForElement(word.ToLower(), 0, [], result);
        //return said array - empty if not elemental word
        return result.Count > 0 ? [.. result] : [];
    }

    private static void LookForElement(string word, int index, List<string> foundElements, List<string[]> result)
    {
        //check if iteration is complete
        if (index == word.Length)
        {
            //add found elements to result
            result.Add([.. foundElements]);
            return;
        }
        //symbols can have length 1, 2 or 3
        for (int length = 1; length <= 3; length++)
        {
            //go through entire word
            if (index + length <= word.Length)
            {
                //get the letter
                string symbol = word.Substring(index, length);
                //check if letter exists in the dictionary
                if (periodicTable.ContainsKey(FirstLetterToUpper(symbol)))
                {
                    var elementName = periodicTable[FirstLetterToUpper(symbol)];
                    //build list to be output
                    foundElements.Add($"{elementName} ({FirstLetterToUpper(symbol)})");
                    //we need to iterate to look for more
                    LookForElement(word, index + length, foundElements, result);
                    //handle already added element by removing from array
                    foundElements.RemoveAt(foundElements.Count - 1);
                }
            }
        }
    }

    //simple function that handles string capitalisation
    public static string FirstLetterToUpper(string str)
    {
        if (str.Length > 1)
            return char.ToUpper(str[0]) + str.Substring(1);

        return str.ToUpper();
    }
}