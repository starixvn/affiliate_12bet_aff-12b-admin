import React, { useEffect } from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertListToMutilpleSelected } from '../../../modules/utils';
import { initializeFromPageBuilderDetail } from './actions';

const App = () => {
    const { 
        targetCountryList, 
        allLanguagesInSystem, 
        dealList, 
        statusList, 
        moduleList, 
        landingPageUrl,
        campaignList ,
        pageBuilderDetail
    } = window.initialState;

    // Convert lists to proper format for dropdowns
    const newDealList = convertListToMutilpleSelected(dealList, 'GroupId', 'GroupName');

    

    const store = configureStore({
        moduleList,
        campaignList,
        targetCountryList,
        languageList: allLanguagesInSystem,
        dealList: newDealList,
        statusList: statusList.sort((a, b) => a.Status - b.Status),
        landingPageUrl,
        pageBuilderDetail,
        deals: [],
        sources: []
    }, reducer);

    // Initialize from pageBuilderDetail if it exists (edit mode)
    if (pageBuilderDetail) {
        store.dispatch(initializeFromPageBuilderDetail(pageBuilderDetail));
    }

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
};

export default App;