import React from 'react';
import { EmailDetailForm } from '../EmailDetailForm';
import { shallow } from 'enzyme';
import * as ajaxRequest from '../../../../modules/ajaxRequest';

describe('payment request - components - Content ', () => {
    const defaultProps = {
        selectedEmailId: 2255,
        param: {
            FromDate: "6/28/2019",
            IsGetAll: true,
            PageIndex: 1,
            PageSize: 20,
            SearchById: 1,
            SearchText: "",
            ToDate: "6/28/2019"
        }
    }

    const dataItem = {
        AffName: "12BETAFF12885",
        Content: null,
        Email: "te@te1.com",
        EmailId: 2255,
        Sender: "test test1",
        SentDateText: "12/14/2018 03:11:25",
        Subject: "Captcha Test 10"
    }

    it('render correctly', () => {
        const result = shallow(<EmailDetailForm {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - should call handleGetEmailHistoryDetail if selectedEmailId change', () => {
        const componentInstance = new EmailDetailForm(defaultProps);
        componentInstance.handleGetEmailHistoryDetail = jest.fn();
        componentInstance.componentDidUpdate({ selectedEmailId: null });

        expect(componentInstance.handleGetEmailHistoryDetail).toHaveBeenCalledWith(2255);
    });
})