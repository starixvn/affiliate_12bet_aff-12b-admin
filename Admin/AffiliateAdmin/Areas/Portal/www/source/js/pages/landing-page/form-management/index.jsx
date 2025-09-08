import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertListToMutilpleSelected } from '../../../modules/utils';

const App = () => {
    const { targetCountryList, languageList, bannerRootUrl, formNameList, statusList } = window.initialState;

    const newFormNameList = convertListToMutilpleSelected(formNameList, 'ModuleId', 'ModuleName');
    const newTargetCountryList = convertListToMutilpleSelected(targetCountryList, 'TargetCountryCode', 'TargetCountryName');
    const newLanguageList = convertListToMutilpleSelected(languageList, 'LanguageCode', 'LanguageName');

    const store = configureStore({ targetCountryList: newTargetCountryList, languageList: newLanguageList, bannerRootUrl, formNameList: newFormNameList, statusList }, reducer);

    return (
        <Provider store={store}>
            <AppContainer />
        </ Provider>
    );
}

export default React.memo(App);