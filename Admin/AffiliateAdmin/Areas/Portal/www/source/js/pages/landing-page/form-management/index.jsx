import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertListToMutilpleSelected } from '../../../modules/utils';
import ClaimPrincipal from '../../../modules/claim';
import { claimResource } from '../../../modules/claim';


const App = () => {
    const { targetCountryList, allLanguagesInSystem, bannerRootUrl, moduleList, statusList } = window.initialState;
    const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
    const isModifyInFormUploader = claimsPrincipal.hasWriteClaim(claimResource.RegistrationFormUploader);

    const newModuleList = convertListToMutilpleSelected(moduleList, 'ModuleId', 'ModuleName');
    const newTargetCountryList = convertListToMutilpleSelected(targetCountryList, 'TargetCountryCode', 'TargetCountryName');
    const languageList = convertListToMutilpleSelected([...new Map(allLanguagesInSystem.map(item => [item.LanguageCode, item])).values()], 'LanguageCode', 'LanguageName');

    const store = configureStore({ targetCountryList: newTargetCountryList, languageList, allLanguagesInSystem, bannerRootUrl, moduleList: newModuleList, statusList, isModifyInFormUploader }, reducer);

    return (
        <Provider store={store}>
            <AppContainer />
        </ Provider>
    );
}

export default React.memo(App);