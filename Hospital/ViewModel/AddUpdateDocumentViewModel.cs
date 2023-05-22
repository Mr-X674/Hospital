using GalaSoft.MvvmLight.Command;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class AddUpdateDocumentViewModel : BaseViewModel
    {
        private Document _document;

        public Document Document
        {
            get => _document;
            set
            {
                _document = value;
                OnProperty("Document");
            }
        }

        public AddUpdateDocumentViewModel(DataModel data)
        {
            _data = data;
            Document = new Document();
        }

        public AddUpdateDocumentViewModel(DataModel data, Document document)
        {
            _data = data;
            Document = document;
        }

        public void Add()
        {
            if(Document.Type == "" || Document.Diagnosis == "")
            {
                Message("");
                return;
            }

            Document.Id = _data.Documents.Count() == 0 ? 1 : _data.Documents.Last().Id + 1;

            _data.Add(Document);
            Close();
        }

        public void Update()
        {
            if (Document.Type == "" || Document.Diagnosis == "")
            {
                Message("");
                return;
            }

            _data.Update(Document);
            Close();
        }

        public RelayCommand AddCommand => new RelayCommand(Add);
        public RelayCommand UpdateCommand => new RelayCommand(Update);
    }
}
