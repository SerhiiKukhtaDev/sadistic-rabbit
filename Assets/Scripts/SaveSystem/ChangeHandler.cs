using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem
{
    public class ChangeHandler : MonoBehaviour
    {
        [SerializeField] private SessionView sessionTemplate;
        [SerializeField] private Transform container;
        [SerializeField] private UnityEvent onReturnToChange;
        
        private List<SessionView> _sessionViews;

        private void OnDisable()
        {
            _sessionViews.ForEach(session => session.ChildrenChangeButtonClicked -= HandleChangeButtonClick);
        }

        private void Start()
        {
            _sessionViews = new List<SessionView>();
            var sessions = SessionsDatabase.ReadAllData();
            sessions.ForEach(AddSession);
        }

        private void AddSession(SessionEntity session)
        {
            var createdSession = Instantiate(sessionTemplate, container);
            createdSession.Render(session);
            createdSession.ChildrenChangeButtonClicked += HandleChangeButtonClick;
            
            _sessionViews.Add(createdSession);
        }

        private void HandleChangeButtonClick(ChangeEntity changeEntity, SessionView sessionView)
        {
            var data = SessionsDatabase.GetPlayerInfo(changeEntity.ID);

            PlayerData.Instance.SetData(data);
            
            SessionsDatabase.DeleteAllChangesAbove(changeEntity.ID);
            
            onReturnToChange?.Invoke();
        }
    }
}
