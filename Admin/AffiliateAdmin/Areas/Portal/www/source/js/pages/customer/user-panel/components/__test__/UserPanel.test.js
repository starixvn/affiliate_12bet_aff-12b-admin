import React from 'react';
import { UserPanel } from '../UserPanel';
import { shallow, mount } from 'enzyme';

describe('customer - user-panel - components - UserPanel', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });
    const defaultProps = {
        selectedUserPanel: 'statementHistory',
        isShowLoader: true,
        onClickMenu: jest.fn(),
        claimsPrincipal: { hasClaim: jest.fn(() => true) },
        isWaitingApproval: false
    }

    it('Should render correctly with is pending', () => {
        const wrapper = shallow(<UserPanel {...defaultProps}/>);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with not pending', () => {
        const props = Object.assign({}, defaultProps, { isWaitingApproval: true });
        const wrapper = shallow(<UserPanel {...props}/>);

        expect(wrapper).toMatchSnapshot();
    });

    it('useEffect - should call onClickMenu', () => {
        const props = Object.assign({}, defaultProps, { isWaitingApproval: true });
        mount(<UserPanel {...props} />);

        expect(props.onClickMenu).toHaveBeenCalledWith('edit');
    });
})