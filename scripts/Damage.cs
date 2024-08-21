public partial class Damage
{
    public int Basic = 10;

    public int Piercing = 0;

    // Plan
    // DamageEffect subclass
    // For things like DoT and particle effects?
    // nextEffect until no nextEffect
    // pass in Health Object
    // Each Effect can call the Health class with various methods
 
    // Base damage class calculates total initial damage based on methods called from the health class
    // Then passes back the total damage
}
