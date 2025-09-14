import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertListToMutilpleSelected } from '../../../modules/utils';

const App = () => {
    const { targetCountryList, languageList, dealList, statusList, landingPageUrl, campaignList } = window.initialState;

    // Convert lists to proper format for MultipleSelect components
    const newTargetCountryList = convertListToMutilpleSelected(targetCountryList, 'TargetCountryCode', 'TargetCountryName');
    const newLanguageList = convertListToMutilpleSelected(languageList, 'LanguageCode', 'LanguageName');
    const newDealList = convertListToMutilpleSelected(dealList, 'GroupId', 'GroupName');
    const newStatusList = convertListToMutilpleSelected(statusList, 'StatusId', 'Status');
    const newCampaignList = convertListToMutilpleSelected(campaignList, 'CampaignId', 'CampaignName');

    const store = configureStore({
        targetCountryList: newTargetCountryList,
        languageList: newLanguageList,
        dealList: newDealList,
        statusList: newStatusList,
        landingPageUrl,
        originalLanguageList: languageList,
        campaignList: newCampaignList
    }, reducer);

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
};

export default App;