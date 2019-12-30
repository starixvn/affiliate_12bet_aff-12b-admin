import React from 'react';
import { ChangePasswordForm } from '../ChangePasswordForm';
import { shallow } from 'enzyme';

describe('common - components - ChangePasswordForm', () => {
    const defaultProps = { showPopupPassword: true };

    it('render correctly', () => {
        const result = shallow(<ChangePasswordForm {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });
});