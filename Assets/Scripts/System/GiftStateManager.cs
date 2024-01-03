using UnityEngine;
using UnityEngine.Serialization;

namespace System
{
    public enum GiftType
    {
        Red,
        Blue
    }
    
    public class GiftStateManager : MonoBehaviour
    {
        [Header("Gifts")]
        [SerializeField] private int maxRedGifts = 50;
        [SerializeField] private int maxBlueGifts = 50;
        [SerializeField] private int minAddGifts = 15;
        [SerializeField] private int maxAddGifts = 30;
        [SerializeField] private int redGifts = 0;
        [SerializeField] private int blueGifts = 0;
        [SerializeField] private GiftType ammunitionSelected = GiftType.Blue;

        private static GiftStateManager _instance;
    
        public static GiftStateManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = FindObjectOfType<GiftStateManager>();
                }
    
                return _instance;
            }
        }
        
        private GiftStateManager()
        {
            
        }

        public void AddRandomGifts()
        {
            int gifts = UnityEngine.Random.Range(minAddGifts, maxAddGifts);
            int red = UnityEngine.Random.Range(0, gifts);
            int blue = gifts - red;
            
            AddRedGift(red);
            AddBlueGift(blue);
        }
        
        public void AddRedGift(int numGifts)
        {
            redGifts = Mathf.Min(maxRedGifts,redGifts + numGifts);
        }
        
        public void AddBlueGift(int numGifts)
        {
            blueGifts +=  Mathf.Min(maxRedGifts,blueGifts + numGifts);
        }
        
        public void RemoveRedGift()
        {
            redGifts--;
        }
        
        public void RemoveBlueGift()
        {
            blueGifts--;
        }
        
        public int GetRedGifts()
        {
            return redGifts;
        }
        
        public int GetBlueGifts()
        {
            return blueGifts;
        }
        
        public int GetMaxRedGifts()
        {
            return maxRedGifts;
        }
        
        public int GetMaxBlueGifts()
        {
            return maxBlueGifts;
        }
        
        public int GetTotalGifts()
        {
            return redGifts + blueGifts;
        }
        
        public GiftType GetAmmunitionSelected()
        {
            return ammunitionSelected;
        }
    
        public void ChangeAmmunition()
        {
            if (ammunitionSelected == GiftType.Blue)
            {
                ammunitionSelected = GiftType.Red;
            }
            else
            {
                ammunitionSelected = GiftType.Blue;
            }
        }
    }
}
