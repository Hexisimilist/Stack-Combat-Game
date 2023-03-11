namespace Stack_Combat_Game
{
    public static class MyExtensions
    {
        public static T[] RemoveAt<T>(this T[] array, int removeIndex)
        {
            T temp = array[array.Length - 1];
            array[removeIndex] = temp;

            for (int i = removeIndex; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
            Array.Resize(ref array, array.Length - 1);

            return array;
        }

        public static T[] Add<T>(this T[] array, T element)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = element;

            return array;
        }
    }
}
