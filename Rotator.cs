namespace JollySamurai.UnrealEngine4.T3D
{
    public struct Rotator
    {
        public float Pitch { get; }
        public float Yaw { get; }
        public float Roll { get; }

        public Rotator(float pitch, float yaw, float roll)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }
    }
}
