import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './mainpage/reducers';
import AppContainer from './mainpage/components/AppContainer';
import { configureStore } from '../../modules/store';
import ClaimPrincipal from '../../modules/claim';

console.log(window.globalVariables);
const claimsPrincipal = new ClaimPrincipal(window.globalVariables.claims);
const store = configureStore({ ...window.initialState, claimsPrincipal }, reducer);

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Provider store={store}>
                <AppContainer />
            </Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById('root'));