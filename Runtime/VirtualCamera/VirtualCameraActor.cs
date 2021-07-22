using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.LiveCapture.VirtualCamera
{
    /// <summary>
    /// The actor used to hold the parameters of a camera.
    /// </summary>
    [DisallowMultipleComponent]
    [ExcludeFromPreset]
    [AddComponentMenu("Live Capture/Virtual Camera/Virtual Camera Actor")]
    [ExecuteAlways]
    [RequireComponent(typeof(Animator))]
    [HelpURL(Documentation.baseURL + Documentation.version + Documentation.subURL + "ref-component-virtual-camera-actor" + Documentation.endURL)]
    public class VirtualCameraActor : MonoBehaviour
    {
        [SerializeField]
        Lens m_Lens = Lens.DefaultParams;
        [SerializeField]
        LensIntrinsics m_LensIntrinsics = LensIntrinsics.DefaultParams;
        [SerializeField]
        CameraBody m_CameraBody = CameraBody.DefaultParams;
        [SerializeField]
        bool m_DepthOfField;
        [SerializeField, AspectRatio]
        float m_CropAspect = Settings.k_DefaultAspectRatio;

        [SerializeField]
        Vector3 m_LocalPosition;
        [SerializeField]
        Vector3 m_LocalEulerAngles;
        [SerializeField]
        bool m_LocalPositionEnabled;
        [SerializeField]
        bool m_LocalEulerAnglesEnabled;

        /// <summary>
        /// Local position driven by an animation clip. This property was added
        /// because position and rotation can't be animated separately in the
        /// transform during root motion.
        /// </summary>
        /// <remarks>
        /// The value is valid while <see cref="LocalPositionEnabled"/> is true.
        /// </remarks>
        internal Vector3 LocalPosition => m_LocalPosition;

        /// <summary>
        /// Local rotation driven by an animation clip. This property was added
        /// because position and rotation can't be animated separately in the
        /// transform during root motion.
        /// </summary>
        /// <remarks>
        /// The value is valid while <see cref="LocalEulerAnglesEnabled"/> is true.
        /// </remarks>
        internal Vector3 LocalEulerAngles => m_LocalEulerAngles;

        /// <summary>
        /// Is true in the LateUpdate stage if the an animation clip is
        /// driving this actor's position.
        /// </summary>
        internal bool LocalPositionEnabled
        {
            get => m_LocalPositionEnabled;
            // Keep setter internal
            set => m_LocalPositionEnabled = value;
        }

        /// <summary>
        /// Is true in the LateUpdate stage if the an animation clip is
        /// driving this actor's rotation.
        /// </summary>
        internal bool LocalEulerAnglesEnabled
        {
            get => m_LocalEulerAnglesEnabled;
            // Keep setter internal
            set => m_LocalEulerAnglesEnabled = value;
        }

        /// <summary>
        /// The Animator component used by the device for playing takes on this actor.
        /// </summary>
        public Animator Animator { get; private set; }

        /// <summary>
        /// The <see cref="VirtualCamera.Lens"/> parameters of the actor.
        /// </summary>
        /// <remarks>
        /// The parameters will be used by a camera driver to configure the final camera component.
        /// </remarks>
        public Lens Lens => m_Lens;

        /// <summary>
        /// The <see cref="VirtualCamera.LensIntrinsics"/> parameters of the actor.
        /// </summary>
        /// <remarks>
        /// The parameters will be used by a camera driver to configure the final camera component.
        /// </remarks>
        public LensIntrinsics LensIntrinsics => m_LensIntrinsics;

        /// <summary>
        /// The <see cref="VirtualCamera.CameraBody"/> parameters of the actor.
        /// </summary>
        /// <remarks>
        /// The parameters will be used by a camera driver to configure the final camera component.
        /// </remarks>
        public CameraBody CameraBody => m_CameraBody;

        /// <summary>
        /// Is depth of field enabled.
        /// </summary>
        /// <remarks>
        /// The camera driver will use this property to enable or disable depth of field for the camera.
        /// </remarks>
        public bool DepthOfFieldEnabled => m_DepthOfField;

        /// <summary>
        /// The aspect ratio of the crop mask.
        /// </summary>
        public float CropAspect => m_CropAspect;

        void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        void OnValidate()
        {
            m_LensIntrinsics.Validate();
            m_Lens.Validate(m_LensIntrinsics);
            m_CameraBody.Validate();
        }
    }
}
