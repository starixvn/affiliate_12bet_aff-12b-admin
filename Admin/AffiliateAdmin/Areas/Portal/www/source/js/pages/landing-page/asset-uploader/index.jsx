import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';

import { convertListToMutilpleSelected } from '../../../modules/utils';
import { moduleTypes } from '../libs/constants';

const App = () => {
    const { moduleList, targetCountryList, allLanguagesInSystem, maxFileSize, maximumFiles, allowedExtensions, bannerRootUrl, statusList } = window.initialState;

    const newModuleList = convertListToMutilpleSelected(moduleList.filter(item => item.ModuleType !== moduleTypes.Form && item.ModuleType !== moduleTypes.Link), 'ModuleId', 'ModuleName');
    const newTargetCountryList = convertListToMutilpleSelected(targetCountryList, 'TargetCountryCode', 'TargetCountryName');
    const languageList = convertListToMutilpleSelected([...new Map(allLanguagesInSystem.map(item => [item.LanguageCode, item])).values()], 'LanguageCode', 'LanguageName');

    const store = configureStore({
        moduleList: newModuleList,
        targetCountryList: newTargetCountryList,
        languageList,
        allLanguagesInSystem,
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