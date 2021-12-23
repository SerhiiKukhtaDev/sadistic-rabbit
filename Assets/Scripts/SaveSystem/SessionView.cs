using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SaveSystem
{
    public class SessionView : MonoBehaviour
    {
        [SerializeField] private Text dateText;
        [SerializeField] private Transform savesContainer;
        [SerializeField] private SaveView saveTemplate;

        public UnityAction<ChangeEntity, SessionView> ChildrenChangeButtonClicked { get; set; }
        
        private List<SaveView> _saves;

        private void OnDisable()
        {
            _saves.ForEach(save => save.ReturnToChange -= OnChildrenChangeClicked);
        }

        public void Render(SessionEntity session)
        {
            _saves = new List<SaveView>();
            dateText.text = $"Session date - {session.Date.ToString(CultureInfo.CurrentCulture)}";
            session.Changes.ForEach(AddToSession);
        }

        private void AddToSession(ChangeEntity change)
        {
            var createdSave = Instantiate(saveTemplate, savesContainer);
            createdSave.Render(change);
            createdSave.ReturnToChange += OnChildrenChangeClicked;
            _saves.Add(createdSave);
        }

        private void OnChildrenChangeClicked(ChangeEntity change, SaveView saveView)
        {
            ChildrenChangeButtonClicked?.Invoke(change, this);
            saveView.ReturnToChange -= OnChildrenChangeClicked;
        }
    }
}
