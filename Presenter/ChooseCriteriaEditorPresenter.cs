﻿using System;
using System.Collections.Generic;
using Brockhaus.Arbeitszeugnisgenerator.View;
using Brockhaus.Arbeitszeugnisgenerator.View.Forms;
using Brockhaus.PraktikumZeugnisGenerator.Model;
using Brockhaus.PraktikumZeugnisGenerator.Dialogs;

namespace Brockhaus.Arbeitszeugnisgenerator.Presenter
{
    public class ChooseCriteriaEditorPresenter
    {

        private const string NAME_ERR_TITLE = "Fehler";
        private const string NAME_ERR_TEXT = "Namen dürfen nicht doppelt vergeben werden";
        public List<Criteria> Criterias;
        public Criteria SelectedCriteria;
        public ChooseCriteriaEditorV view;

        public ChooseCriteriaEditorPresenter(ChooseCriteriaEditorV view, List<Criteria> criteriaList)
        {
            this.view = view;
            Criterias = criteriaList;
        }

        public void AddCriteria(string name)
        {
            try
            {
                if (name == "") throw new ArgumentException();
                foreach (Criteria criteria in Criterias)
                {
                    if (name == criteria.Name) throw new ArgumentException();
                }
                Criteria newCrit = new Criteria(name);
                Criterias.Add(newCrit);
                SelectedCriteria = newCrit;
                view.RefreshView();
            }
            catch (ArgumentException)
            {
                MessageDialog msg = new MessageDialog(NAME_ERR_TITLE, NAME_ERR_TEXT);
                msg.ShowDialog();
            }
        }

        public void RemoveCriteria(int removeIndex)
        {
            if (removeIndex < 0 || removeIndex >= Criterias.Count) throw new ArgumentException();

            Criterias.RemoveAt(removeIndex);
            view.RefreshView();
        }

        public void SelectCriteria(int selectIndex)
        {
            if (selectIndex == -1)
            {
                SelectedCriteria = null;
                return;
            }
            if (selectIndex < 0 || selectIndex >= Criterias.Count)
            {
                throw new IndexOutOfRangeException();
            }
            SelectedCriteria = Criterias[selectIndex];
            view.RefreshView();
        }
    }
}
