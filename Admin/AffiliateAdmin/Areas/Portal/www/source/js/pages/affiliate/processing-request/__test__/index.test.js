import React from 'react';
import App from '../index';
import { shallow } from 'enzyme';

describe('processing request - index - ', () => {
    it('Should render correctly', () => {
        window.initialState = {
            userName: 'test',
            languageList: [],
            today: '100/11/2021'
        };

        window.globalVariables = {}

        const snapshot = shallow(<App />);

        expect(snapshot).toMatchSnapshot();
    })
})