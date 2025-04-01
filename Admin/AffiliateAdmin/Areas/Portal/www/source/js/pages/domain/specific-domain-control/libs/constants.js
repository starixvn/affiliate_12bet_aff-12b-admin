import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Search: globalResource.Search,
    Index: '#',
    Affiliate: 'Affiliate',
    AffDomain: 'Aff Domain',
    RedirectDomain: 'Redirect Domain',
    Creator: 'Creator',
    CreatedDate: 'Created Date',
    SSL: 'SSL',
    Status: 'Status',
    Editor: 'Editor',
    UpdatedDate: 'Updated Date',
    Edit: globalResource.Edit,
    Delete: globalResource.Delete
}

export const defaultState = {
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    selectedDomain: null,
    affNames: '',
}