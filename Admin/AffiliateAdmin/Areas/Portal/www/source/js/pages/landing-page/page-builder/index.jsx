import React, { useEffect } from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertListToMutilpleSelected } from '../../../modules/utils';
import { initializeFromPageBuilderDetail, initializeDefaultValues } from './actions';
import ClaimPrincipal from '../../../modules/claim';
import { claimResource } from '../../../modules/claim';

const App = () => {
    const { 
        targetCountryList, 
        allLanguagesInSystem, 
        dealList, 
        statusList, 
        moduleList, 
        bannerRootUrl,
        landingPageUrl,
        campaignList ,
        pageBuilderDetail
    } = window.initialState;

    const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
    const isModifyInPageBuilder = claimsPrincipal.hasWriteClaim(claimResource.PageManagement);

    // Convert lists to proper format for dropdowns
    const newDealList = convertListToMutilpleSelected(dealList, 'GroupId', 'GroupName');



    const store = configureStore({
        moduleList,
        campaignList,
        targetCountryList,
        languageList: allLanguagesInSystem,
        dealList: newDealList,
        statusList: statusList.sort((a, b) => a.Status - b.Status),
        bannerRootUrl,
        landingPageUrl,
        pageBuilderDetail,
        deals: [],
        sources: {},
        isModifyInPageBuilder
    }, reducer);

    // Initialize from pageBuilderDetail if it exists (edit mode)
    if (pageBuilderDetail) {
        store.dispatch(initializeFromPageBuilderDetail(pageBuilderDetail));
    } else {
        // Initialize with default values for create mode
        store.dispatch(initializeDefaultValues());
    }

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
};

export default App;