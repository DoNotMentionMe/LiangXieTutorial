using System.Linq;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace HYH
{
    public interface ISaveSystem : ISystem
    {
        bool HasSavedKey(string key);
        void AddSavedKey(string key);

        void Save();

        void Load();

        void Clear();
    }

    public class SaveSystem : AbstractSystem, ISaveSystem
    {
        protected override void OnInit()
        {
            
        }

        HashSet<string> mSavedKeys = new HashSet<string>();

        public bool HasSavedKey(string key)
        {
            return mSavedKeys.Contains(key);
        }

        public void AddSavedKey(string key)
        {
            mSavedKeys.Add(key);
        }

        public void Save()
        {
            var savedString = string.Join(";@;", mSavedKeys);
            PlayerPrefs.SetString(nameof(mSavedKeys), savedString);
        }

        public void Load()
        {
            var savedString = PlayerPrefs.GetString(nameof(mSavedKeys),string.Empty);

            if(savedString == string.Empty)
            {
                mSavedKeys = new HashSet<string>();
            }
            else
            {
                mSavedKeys = savedString.Split(";@;").ToHashSet();
            }
        }

        public void Clear()
        {
            PlayerPrefs.SetString(nameof(mSavedKeys), string.Empty);
            mSavedKeys.Clear();
        }

    }

}
