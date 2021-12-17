import React from 'react';
import { Provider } from 'react-redux';
import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import ClaimPrincipal from '../../../modules/claim';

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { languageList, previewLink, offerFile } = window.initialState;
        const store = configureStore({ claimsPrincipal: ClaimPrincipal.currentClaimPrincipal, languageList, previewLink, isFromAnotherPage: !!offerFile, offerFile }, reducer);

        return (
            <Provider store={store}>
                <AppContainer />
            </ Provider>
        );
    }
}