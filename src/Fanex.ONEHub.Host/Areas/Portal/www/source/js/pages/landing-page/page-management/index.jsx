import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertListToMutilpleSelected } from '../../../modules/utils';
import ClaimPrincipal from '../../../modules/claim';
import { claimResource } from '../../../modules/claim';

const App = () => {
    const { targetCountryList, allLanguagesInSystem, dealList, statusList, landingPageUrl, campaignList } = window.initialState;
    const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
    const isModifyInPageManagement = claimsPrincipal.hasWriteClaim(claimResource.PageManagement);

    // Convert lists to proper format for MultipleSelect components
    const newTargetCountryList = convertListToMutilpleSelected(targetCountryList, 'TargetCountryCode', 'TargetCountryName');
    const languageList = convertListToMutilpleSelected([...new Map(allLanguagesInSystem.map(item => [item.LanguageCode, item])).values()], 'LanguageCode', 'LanguageName');
    const newDealList = convertListToMutilpleSelected(dealList, 'GroupId', 'GroupName');
    const newStatusList = convertListToMutilpleSelected(statusList, 'StatusId', 'Status');
    const newCampaignList = convertListToMutilpleSelected(campaignList, 'CampaignId', 'CampaignName');

    const store = configureStore({
        targetCountryList: newTargetCountryList,
        languageList,
        dealList: newDealList,
        statusList: newStatusList,
        landingPageUrl,
        allLanguagesInSystem,
        campaignList: newCampaignList,
        isModifyInPageManagement
    }, reducer);

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
};

export default App;