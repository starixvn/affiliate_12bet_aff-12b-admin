using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.UserPanel
{
    public class NoteViewModel
    {
        public IEnumerable<Note> NoteList { get; set; }

        public NoteParam Param { get; set; }
    }
}
