using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;

namespace MemoryMaze
{
    public class SuperText : Text
    {
        public float minFrequency;
        public float maxFrequency;
        public float minDuration;
        public float maxDuration;
        public float probability;

        List<Change> changeList;

        static char[] possibleChars = { '_', '$', '#', '%', '=', '&', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public SuperText(string _str, Font _font, float _probability)
            : base(_str, _font)
        {
            minFrequency = 0.2f;
            maxFrequency = 2f;
            minDuration = 0.02f;
            maxDuration = 0.4f;
            probability = _probability;

            changeList = new List<Change>();
        }

        class Change
        {
            public Change (int lengthOfString)
            {
                newChar = possibleChars[Rand.IntValue(0, possibleChars.Length)];
                position = Rand.IntValue(0, lengthOfString);
                duration = 0;
                downTime = 0;
                isActive = false;
            }

            public void RandomizeChange(int lengthOfString, float minDuration, float maxDuration, float minFrequency, float maxFrequency)
            {
                newChar = possibleChars[Rand.IntValue(0, possibleChars.Length)];
                position = Rand.IntValue(0, lengthOfString - 1);
                duration = Rand.Value(minDuration, maxDuration);
                downTime = Rand.Value(minFrequency, maxFrequency);
                isActive = false;
            }

            public char newChar;
            public int position;
            public float duration;
            public float downTime;
            public bool isActive;
        }

        public void Update(float deltaTime)
        {
            UpdateLength();
            for (int i = 0; i < changeList.Count; i++)
            {
                if(changeList[i].downTime <= 0f)
                {
                    if (changeList[i].duration <= 0f)
                    {
                        changeList[i].RandomizeChange(DisplayedString.Length, minDuration, maxDuration, minFrequency, maxFrequency);
                    }
                    else
                    {
                        changeList[i].isActive = true;
                        changeList[i].duration -= deltaTime;
                    }
                }
                else
                {
                    changeList[i].downTime -= deltaTime;
                }
            }
        }

        void UpdateLength()
        {
            int changeListLength = (int) (DisplayedString.Length * probability);
            if ( changeListLength != changeList.Count)
            {
                if (changeListLength > changeList.Count)
                {
                    while (changeList.Count < changeListLength)
                    {
                        changeList.Add(new Change(DisplayedString.Length));
                    }
                }
                else
                {
                    if (changeListLength == 0)
                        changeList.RemoveAll(b => true);
                    else
                        changeList.RemoveRange(changeListLength-1, changeList.Count - changeListLength);
                    foreach (Change c in changeList)
                        c.RandomizeChange(DisplayedString.Length, minDuration, maxDuration, minFrequency, maxFrequency);
                }
            }
        }

        public new void Draw(RenderTarget target, RenderStates states)
        {
            string str = DisplayedString;

            ApplyChangesToString(DisplayedString);

            base.Draw(target, states);

            DisplayedString = str;
        }

        void ApplyChangesToString(string str)
        {
            UpdateLength();
            foreach (Change c in changeList)
            {
                if (c.isActive)
                {
                    DisplayedString = DisplayedString.Remove(c.position, 1);
                    DisplayedString = DisplayedString.Insert(c.position, c.newChar.ToString());
                }
            }
        }
    }
}
