namespace ExampleSpiritCrossmod;

/// <summary>
/// Defines the conversion we'll be using for the example mod.
/// </summary>
public class IrradiatedConversion : ModBiomeConversion
{
    public static int ConversionType => ModContent.GetInstance<IrradiatedConversion>().Type;
}