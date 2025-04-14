import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import { configureStore } from '../../modules/store';
import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { convertArrayToObjectProps } from '../../modules/utils';
import ClaimPrincipal, { claimResource } from '../../modules/claim';

const { languageList, targetCountryList } = window.initialState;

const languageCodeList = convertArrayToObjectProps(languageList, 'LanguageCode', 'LanguageName');
const targetCountryCodeList = convertArrayToObjectProps(targetCountryList, 'TargetCountryCode', 'TargetCountryName');

const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
const canAddAffMktAccount = claimsPrincipal.hasClaim(claimResource.CreateAffMktAccount);

const store = configureStore({
    languageCodeList,
    targetCountryCodeList,
    canAddAffMktAccount
}, reducer);

const App = () => {
    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
};

export default App;

ReactDOM.render(<App />, document.getElementById("root"))