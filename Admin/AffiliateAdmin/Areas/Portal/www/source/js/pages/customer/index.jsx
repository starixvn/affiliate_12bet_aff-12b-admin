import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './mainpage/reducers';
import AppContainer from './mainpage/components/AppContainer';
import { configureStore } from '../../modules/store';
import ClaimPrincipal from '../../modules/claim';


export default class App extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
        const store = configureStore({ ...window.initialState, claimsPrincipal }, reducer);
        return (
            <Provider store={store}>
                <AppContainer />
            </Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById('root'));