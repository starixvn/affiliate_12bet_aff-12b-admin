import React from 'react';
import { Activated } from '../Activated';
import { shallow, mount } from 'enzyme';
import { act } from 'react-dom/test-utils';

describe('active member - components - Activated ', () => {
    const activatedGridData = [
        {
            UserName: "xyl157",
            CreatedDate: "2017-07-06T10:24:00",
            CreatedDateText: "07/06/2017 10:24:00",
            FirstName: "",
            IbcUserName: "12BETRMB0239317",
            LastName: "",
            IbcCustId: 26522982
        }
    ];

    const defaultProps = {
        activatedGridData
    };

    it('render correctly', () => {
        const result = shallow(<Activated {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with ActivatedGridData is null', () => {
        const props = Object.assign({}, defaultProps, { activatedGridData: null })
        const result = shallow(<Activated {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with ActivatedGridData is empty', () => {
        const props = Object.assign({}, defaultProps, { activatedGridData: [] })
        const result = shallow(<Activated {...props} />);

        expect(result).toMatchSnapshot();
    });
})