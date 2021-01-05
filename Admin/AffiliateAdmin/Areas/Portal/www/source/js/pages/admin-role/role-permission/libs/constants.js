import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const management = 1;

export const headerText = {
    Index: globalResource.Index,
    Function: 'Function',
    IndividualPermission: 'Individual Permission',
    RolePermission: 'Role Permission',
    Read: 'Read',
    Host: 'Host',
    View: 'View',
    Language: 'Language',
    Role: 'Role'
}

export const resource = {
    Save: globalResource.Save,
    Cancel: globalResource.Cancel,
    Edit: globalResource.Edit
}

export const messages = {
    AreYouSureUpdateRole: 'Are you sure? <br /> By confirm this action, <strong className="text-danger">all admin users</strong> under this role will update with new default role setting.',
    AreYouSureUpdateLanguages: 'Are you sure? <br /> By confirm this action, <strong className="text-danger">all admin users</strong> under this role will update with new default language setting.',
}