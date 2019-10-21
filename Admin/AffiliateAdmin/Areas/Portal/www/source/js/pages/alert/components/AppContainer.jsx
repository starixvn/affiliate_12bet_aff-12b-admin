import React, { PureComponent } from 'react';
import { alertMessageList } from '../libs/constants';
import { resolveClientUrl } from '../../../modules/utils';

export default class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        var timerID = window.setTimeout(() => {
            window.location = resolveClientUrl(this.props.param.Link)
        }, 3000);
    }

    render() {
        const { param } = this.props;

        return param && (
            <div className="alert-container">
                <div>
                    <p>Redirecting...</p>
                </div>
                <div className="text-center">
                    <p><strong>{param.User} {alertMessageList[param.Code] || alertMessageList[0]}</strong></p>
                    <a href={param.Link}><p>Click here if your browser does not automatically redirect you.</p></a>
                </div>
            </div>
        )
    }
}