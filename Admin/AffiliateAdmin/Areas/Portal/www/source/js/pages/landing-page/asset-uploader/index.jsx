import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';

import { convertListToMutilpleSelected } from '../../../modules/utils';

const App = () => {
    const { moduleList, targetCountryList, languageList, maxFileSize, maximumFiles, allowedExtensions, bannerRootUrl, statusList } = window.initialState;

    const newModuleList = convertListToMutilpleSelected(moduleList, 'ModuleId', 'ModuleName');
    const newTargetCountryList = convertListToMutilpleSelected(targetCountryList, 'TargetCountryCode', 'TargetCountryName');
    const newLanguageList = convertListToMutilpleSelected(languageList, 'LanguageCode', 'LanguageName');

    const store = configureStore({
        moduleList: newModuleList,
        targetCountryList: newTargetCountryList,
        languageList: newLanguageList,
        maxFileSize,
        maximumFiles,
        allowedExtensions,
        bannerRootUrl,
        statusList
    }, reducer);

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
};

export default App;