import React, { Component } from 'react';
import { Provider } from 'react-redux';
import ReactDOM from 'react-dom';

import reducer from '../core/reducers';
import { configureStore } from '../../../modules/store';
import AppContainer from '../core/components/AppContainer';
import { hasWriteAdminRoleClaim } from '../common/libs/utils';

export default class App extends Component {
    constructor(props) {
        super(props);

        this.store = configureStore({ hasWriteAdminRoleClaim: hasWriteAdminRoleClaim() }, reducer);
    }

    render() {

        return (
            <Provider store={this.store}>
                <AppContainer />
            </Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"));