using Ryujinx.Core.OsHle.Ipc;
using System.Collections.Generic;

namespace Ryujinx.Core.OsHle.Services.Am
{
    class IApplicationProxy : IpcService
    {
        private Dictionary<int, ServiceProcessRequest> m_Commands;

        public override IReadOnlyDictionary<int, ServiceProcessRequest> Commands => m_Commands;

        public IApplicationProxy()
        {
            m_Commands = new Dictionary<int, ServiceProcessRequest>()
            {
                {    0, GetCommonStateGetter    },
                {    1, GetSelfController       },
                {    2, GetWindowController     },
                {    3, GetAudioController      },
                {    4, GetDisplayController    },
                {   11, GetLibraryAppletCreator },
                {   20, GetApplicationFunctions },
                { 1000, GetDebugFunctions       }
            };
        }

        public long GetCommonStateGetter(ServiceCtx Context)
        {
            MakeObject(Context, new ICommonStateGetter());

            return 0;
        }

        public long GetSelfController(ServiceCtx Context)
        {
            MakeObject(Context, new ISelfController());

            return 0;
        }

        public long GetWindowController(ServiceCtx Context)
        {
            MakeObject(Context, new IWindowController());

            return 0;
        }

        public long GetAudioController(ServiceCtx Context)
        {
            MakeObject(Context, new IAudioController());

            return 0;
        }

        public long GetDisplayController(ServiceCtx Context)
        {
            MakeObject(Context, new IDisplayController());

            return 0;
        }

        public long GetLibraryAppletCreator(ServiceCtx Context)
        {
            MakeObject(Context, new ILibraryAppletCreator());

            return 0;
        }

        public long GetApplicationFunctions(ServiceCtx Context)
        {
            MakeObject(Context, new IApplicationFunctions());

            return 0;
        }

        public long GetDebugFunctions(ServiceCtx Context)
        {
            MakeObject(Context, new IDebugFunctions());

            return 0;
        }
    }
}