import React from 'react';
import App from '../index';
import { shallow } from 'enzyme';

jest.mock('react-dom', ()=> ({render: jest.fn()}))

describe('offer page - App', () => {
    it('Should render correctly', () => {
        window.globalVariables = {};
        window.initialState = { languageList: [], previewLink: '' };

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})