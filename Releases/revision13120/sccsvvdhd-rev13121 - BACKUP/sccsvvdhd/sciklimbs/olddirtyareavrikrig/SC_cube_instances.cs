using SharpDX;
using Jitter.Dynamics;


namespace sccs
{
    public class SC_cube_instances : ITransform, IComponent
    {
        public ITransform transform { get; private set; }
        IComponent ITransform.Component
        {
            get => component;
        }
        IComponent component;
        RigidBody IComponent.rigidbody { get; set; }

        SoftBody IComponent.softbody { get; set; }

        public Matrix _POSITION { get; set; }
        public Matrix _ORIGINPOSITION { get; set; }
        public Matrix _LASTPOSITION { get; set; }
        public Matrix current_pos { get; set; }

        public Vector3 sc_last_frame_linearvelocity { get; set; }
        public Vector3 sc_last_frame_angularvelocity { get; set; }

        public Vector3 force_to_apply_next_frame { get; set; }

        public int force_to_apply_next_frame_swtch { get; set; }

        public SC_cube_instances()
        {
            transform = this;
            component = this;
        }
    }
}