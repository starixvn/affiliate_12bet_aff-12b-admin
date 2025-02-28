import React from 'react';
import App from '../index';
import { shallow } from 'enzyme';

jest.mock('react-dom', () => ({ render: jest.fn() }))

describe('domain control - App', () => {
    it('Should render correctly', () => {
        window.initialState = {
            marketList: [
                {
                    LanguageCode: "en-GB",
                    DomainName: "English (Euro)"
                },
                {
                    LanguageCode: "vi-VN",
                    DomainName: "VietNamese"
                }
            ],
            affSubDomainList: [
                {
                    columnKey: "test",
                    columnName: "test",
                    checked: true
                }
            ]
        };

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})