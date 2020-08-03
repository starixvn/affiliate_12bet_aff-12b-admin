import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';

const { userId, userName, roleList } = window.initialState;
const newRoleList = {};
for (let i = 0; i < roleList.length; i++) {
    const item = roleList[i];
    newRoleList[item.RoleId] = item.RoleName;
}

const store = configureStore({ userId, userName, roleList: newRoleList }, reducer);

export default class App extends Component {
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

ReactDOM.render(<App />, document.getElementById("root"))