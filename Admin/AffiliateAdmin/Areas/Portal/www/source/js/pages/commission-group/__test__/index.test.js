import React from 'react';
import App from '../index';
import { shallow } from 'enzyme';
import * as ajaxRequest from '../../../modules/ajaxRequest';

jest.mock('react-dom', () => ({ render: jest.fn() }))

describe('commission group - App', () => {
    beforeEach(() => {
        window.initialState = {
            commGroupList: [
                {
                    DeductedPercentage: 0.17,
                    DeductionId: 10,
                    DeductionName: "Platform Fee"
                },
                {
                    DeductedPercentage: 0.07,
                    DeductionId: 11,
                    DeductionName: "Operational Fee"
                },
                {
                    DeductedPercentage: 0.02,
                    DeductionId: 12,
                    DeductionName: "Banking Fee"
                }
            ],
            poductList: [
                {
                    ProductId: 1,
                    ProductName: "Sportsbook"
                },
                {
                    ProductId: 2,
                    ProductName: "Playtech Casino"
                },
                {
                    ProductId: 2,
                    ProductName: "Number Game"
                }
            ]
        }
    });

    it('Should render correctly', () => {
        window.globalVariables = {};
        window.initialState = {};
        const expectedResponse = { data: { CommGroupList: [], LanguageList: [] }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})