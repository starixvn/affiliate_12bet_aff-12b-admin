import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertListToMutilpleSelected } from '../../../modules/utils';

const App = () => {
    const { 
        targetCountryList, 
        languageList, 
        dealList, 
        statusList, 
        moduleList, 
        landingPageUrl,
        campaignList 
    } = window.initialState;

    // Convert lists to proper format for dropdowns
    const newTargetCountryList = convertListToMutilpleSelected(targetCountryList, 'TargetCountryCode', 'TargetCountryName');
    const newLanguageList = convertListToMutilpleSelected(languageList, 'LanguageCode', 'LanguageName');
    const newModuleList = convertListToMutilpleSelected(moduleList, 'ModuleId', 'ModuleName');
    const newCampaignList = convertListToMutilpleSelected(campaignList || [], 'CampaignId', 'CampaignName');

    const store = configureStore({
        // Legacy state
        targetCountryList: newTargetCountryList,
        languageList: newLanguageList,
        dealList,
        statusList,
        moduleList: newModuleList,
        landingPageUrl,
        
        // Page Builder specific state
        campaigns: newCampaignList,
        deals: [],
        modules: newModuleList,
        sources: []
    }, reducer);

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
};

export default App;