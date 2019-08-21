import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';
import UserPanelBreadcrumb from '../../../../common/components/UserPanelBreadcrumb';

describe('daily earning - components - AppContainer', () => {
    const defaultProps = {
        param: {
            affId: 1271808,
            monthDate: "Jan-2019",
            productId: 0
        }
    }
    it('render correctly', () => {
        const result = shallow(<AppContainer {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with monthDate is null', () => {
        const props = Object.assign({}, defaultProps, { monthDate: null });
        const result = shallow(<AppContainer {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('setSelectedUserPanel - should be called', () => {
        const props = Object.assign({}, defaultProps, { setSelectedUserPanel: jest.fn() });
        const wrapper = shallow(<AppContainer {...props} />);

        wrapper.find(UserPanelBreadcrumb).props().breadcrumbList[0].handleClick();

        expect(props.setSelectedUserPanel).toHaveBeenCalledWith('statementHistory');
    });
});