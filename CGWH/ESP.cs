using System;

namespace CGWH
{
    public class ESP
    {
        private Cheat cheat;
        internal ESP(Cheat cheat)
        {
            this.cheat = cheat;
        }


        internal void LoadESP()
        {
            try
            {
                while (true)
                {
                    int playerNum = cheat.Memory.Read<int>(cheat.ModuleAddress + Offsets.dwLocalPlayer);
                    int playerTeamNum = cheat.Memory.Read<int>(playerNum + Offsets.m_iTeamNum);

                    for (int i = 0; i < 64; i++)
                    {
                        int enemyNum = cheat.Memory.Read<int>(cheat.ModuleAddress + Offsets.dwEntityList + i * 16);
                        int enemyTeamNum = cheat.Memory.Read<int>(enemyNum + Offsets.m_iTeamNum);
                        int index = cheat.Memory.Read<int>(enemyNum + Offsets.m_iGlowIndex);

                        if (enemyTeamNum != 0)
                        {
                            if (enemyTeamNum != playerTeamNum)
                                drawEnemy(index, 255, 0, 0, 255);
                            else
                                drawEnemy(index, 0, 0, 255, 255);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.WriteWithColor("Error: " + ex.Message + "\n>>" + ex, ConsoleColor.Red, true);
            }
        }

        private void drawEnemy(int index, int red, int green, int blue, int alpha)
        {
            int num = cheat.Memory.Read<int>(cheat.ModuleAddress + Offsets.dwGlowObjectManager);

            cheat.Memory.Write(num + index * 56 + 4, red / 100f);
            cheat.Memory.Write(num + index * 56 + 8, green / 100f);
            cheat.Memory.Write(num + index * 56 + 12, blue / 100f);
            cheat.Memory.Write(num + index * 56 + 16, alpha / 100f);

            cheat.Memory.Write(num + index * 56 + 36, true);
            cheat.Memory.Write(num + index * 56 + 37, false);
        }

    }
}
