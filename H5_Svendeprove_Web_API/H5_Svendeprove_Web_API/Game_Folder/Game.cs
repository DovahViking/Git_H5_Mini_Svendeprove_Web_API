namespace H5_Svendeprove_Web_API.Game_Folder
{
    public class Game
    {
        public async Task<List<int[]>> on_ready(int sequence_size)
        {
            List<int[]> values_arrays = new List<int[]>();
            Random rng_light = new Random(); // rng for which of the 3 lights should be affected

            for (int i = 0; i < sequence_size; i += 1)
            {
                int light = rng_light.Next(1, 4); // light 1, 2, or 3

                switch (light)
                {
                    case 1:
                        values_arrays.Add(new int[] { 1, 0, 0 });
                        break;
                    case 2:
                        values_arrays.Add(new int[] { 0, 1, 0 });
                        break;
                    case 3:
                        values_arrays.Add(new int[] { 0, 0, 1 });
                        break;
                }
            }

            return await Task.FromResult(values_arrays);
        }
    }
}
