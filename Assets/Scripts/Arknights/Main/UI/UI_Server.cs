using FairyGUI;

namespace Arknights
{
    public partial class UI_Server
    {
        partial void Init()
        {
            m_create.onClick.Set(OnCreate);
            m_join.onClick.Set(OnJoin);
            m_cancel.onClick.Set(OnCancel);
        }


        private void OnCreate(EventContext context)
        {
            Socket.CreateConnect("127.0.0.1",13000);
        }

        private void OnJoin(EventContext context)
        {
            throw new System.NotImplementedException();
        }

        private void OnCancel(EventContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}