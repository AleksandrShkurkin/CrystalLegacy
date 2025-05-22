namespace Companion
{
    public class Companion : LivingEntity
    {
        private int _previousLevel = -1;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Level = 0;
            Health = 0;
            Defense = 0;
        }

        private void Update()
        {
            Level = Player.Player.Instance.Level;
            if (Level == _previousLevel) return;
            _previousLevel = Level;
            AttackDamage = 7 + (_previousLevel * 2);
        }

        public override void RecieveDamage(int damage) {}
    }
}
