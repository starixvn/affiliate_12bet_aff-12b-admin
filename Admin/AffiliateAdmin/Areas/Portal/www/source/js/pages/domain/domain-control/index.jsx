import React from 'react';
import ReactDOM from 'react-dom';
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
        const store = configureStore({ ...window.initialState, claimsPrincipal: ClaimPrincipal.currentClaimPrincipal }, reducer);
        return (
            <Provider store={store}>
                <AppContainer />
            </ Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"));