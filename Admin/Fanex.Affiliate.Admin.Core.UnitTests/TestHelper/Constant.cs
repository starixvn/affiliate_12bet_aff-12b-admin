namespace Fanex.Affiliate.Admin.Core.UnitTests.TestHelper
{
    public static class Constant
    {
        public const string DefaultRootDomain = "http://abcxyz.com";

        public const string Browser = "Chrome";

        public const int AffId = 1271808;

        public const string AffName = "12BETAFF226";

        public const string UserHostAddress = "::1";

        public const string MarketId = "en-US";

        public const int OnUserId = 8647;

        public const int UserId = 1000000026;

        public static string LogoImageBase64 = @"iVBORw0KGgoAAAANSUhEUgAAACoAAAAqCAYAAADFw8lbAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAACJNJREFUeNq0WGlsVNcVvm
                                                +dmefxLMyMt8ngFWO84A0MwQkB2hQESRsgVRWhNA0NCUrSqkVquilVm/KjVFFCpLSkUpWqVKhKSkKU0EWJIYGEYqgXXC
                                                +AsY3XweDxePb1LT13bCbDrG8GeqWjN8t9533vu+ec+91DoDRD0b7pQdJgep1QqVqRzEErFGG4EHwwSMu8RQL7BGyn752jvlSTyAxOvi/Oz61AfHhBLtA1z
                                                +4fbv3uviEkfxBgX1uy1ARkcGIC0wg3b16nSsxaRBCZXgwNHn/XIIkSgbIfursBurQ4YrmmtOx87SOPKklCNgZrpgmdfzjSLPK8vJCSvT75Gt
                                                oOkbJ8mb4Z3aNB0jS6DVSDeNaXbm6qPzx7K7YYKVEf+1sgFG62e7wD6P8wnhPGv+3YWbQlK0YxSLicJAlpLP4/m9tbrWJZq4plSopZeqGEZewy0poY8gUsAV
                                                FiUs1hkcThZwLYx3QnZj/OCBRA1sDlBJgqlc9pu4OsKDAED5SYRqpU7Fo5jL1htQ195vTWZpiGn3kcwG4AsAMplx5AauHyQSRk0jEkSUUTtoVRKVIC5dYg2UmYj4kCs
                                                Op0jL4KtlKON14Qag8OT3StMRkG5Sz9RY+vMouQrQI7BPZCAlBg8yG4PJNNAky6fSsNKtUUxdL6jC8mSXSW+fU8sPpXCIEv4hk9lG2mHqmvulaqUtQrWNoJ9dWUbu7
                                                rVtvls07vqiwfcRBsUzRGgU28fa3LFiixuP2xwRCPS6CQvmATYg5V6yFgdWMso88lBUKk34ZeHp4oqOCUkRjlGLpHn6/mUs2FGK3KtcSCnaWXMn1Hwt8UNUsoo
                                                VqEgyk9jPgC5slA0ByZThBiaTDUr+G4iiTJRPpFUZEj0MdwBcCMPowVXcJStbbZpClbWkb/0ljdA4y2xJS6RpahRiiSTGDvtRnblc9d3pocgOJV2kQmjU2anqD
                                                rV9dnFApJQiMUFkxQZ13xvzMEIdzFDrsBM9qW4LR9YxDFYZB4noqfd2hsWlejVvXG/66kaEGXz1GxHnq8/uq7ALoGAzXfkUCsYpSqqk5wKgYDCexdcLjLwZLvh
                                                SzTazHcM6VVTsYDpZqavffCs39RafXfI6AWzKgydrunV9VHY5MiiaSq9vDJV7o0AXfmjR5imDHozrESz97+6VpxLXl08/OtWQJV3LGtETr9NUik6HJxDCUuPg+
                                                FundZpHHFjYv75orb2qYurSQlKZ/IX4aYh/cgyTaDRNc8IguWI3F6BFG165DkmANaPUgK+Zz0qpY8vuNPNP5ucM8NAtDsRXbsjkKWmJNmZl2R5kpJgbGulRMiZ
                                                QxALkpAVoGoykZEWlYiankNIovKwEe5m6poCEf2LIZFpHmFlqxu8xM0ezdLH8CMToOVRgBodUm9faOhmEd+V998YLFo8yTlJCWRRfZZ5H1135cJp9FN6H/+K1L
                                                yzwWnz/+zKEgyi1Lt9HtIx/C9aoJc4VZqctlKbRjo5G2giMtTJpv1rebiKmlhVnND1OCXQg/sP25INq+e8oTeob2VRD6rPbH/Nxff9hljS19zoU57LV+lbMgB
                                                6DBe+p5ojHKcOqHMMNRMpYGLCOkFifGn8zYk5FXwHscYCnr/8wQxUR0vVG46nMUhnrfnAPS/GOjZKFAVp42fsa5UFz32zktsIJ03EdLxIzuhkuZn1hZKfl0Z6Y
                                                8/CKqn5xdyAfopBnoKLKI8CKUyL/bfPa1m9tiTLY1fAmXCmTz+kbdEVcyLzIQacsq7i57tOsCMY1Y8j5NW12H3xw6D6B9arlEM/eL+wks0SdxK4xL7O0Wr3x5zg
                                                oL6EKsUxDB3nBJf+kpVXex3u8Rk7BaMi6o6CJGQngizX6VslU/Q1sGfsGNrFhWWdP1JeqYFqEc/8nUGR9qN/HaLyqygiAHfmeFULt8Dle+9XUffQiS5Ga7GdCAmOk
                                                6HJ3umBkiGTcvsm+33SS8/UNKCl6vO2r9MKstbbDBJs8X9sx5XQ5FaUy3YC7u8ASs+eD7baKh/9xx/KYX0fisqnIHV094fNF9Iqktjxuh1KyO4XPWZZNCRT5
                                                yjAHQxxo20rnN8Ye7+Mr2pkBGUg+6ADy0dMDeVcJHjCzCKdpvpsJjQRUBngM3P7zgus9t2HM7YALvpNsqJfKc/XNk95YzEXYFaoXL4w9HtttWiizbDSjSKaK
                                                gpNz5YkaQd+eOEUyiz7WAHf+63f4OP30wKcmB81BT2uUwU6pUD9ovOy75Wy/oC/Hl9qT56StUq6aRtJNJoNBB5eVckb1Rc/x7YvJDqXI834Q3xigoJAirt+FBx
                                                3sDLl21T4zYkQr0nSaTnGEbOLWzbWj746WeRAg/205SdErr9JRtcHl0K4HNgEbEZ7vx3N4A1ZlX5RNHIX75yNZtbmMbVdfBiM/BxF7Dpju/2JnZU9zyFGX0BDm
                                                yXd1drb/167ow5z+P8Ge/0l0PkJO8AUhTKe/GHXZJrriLw/gczotPZQC7T93F7n27MAmtYnJ39Orv7tX8luE8qVfr73MrVTQqI5qOD84Gn3/QXbZ9GyoltSqddC
                                                vKFKRpSSLH1kQXSaLLRZfdVh7u6JckfKGCamwSCYSg5WhvscWbrK//Iqj/qOPZnfMN6nEd4VQC0QJBEA6VWpEymwEfvg66jECQFRy23YHXPhLt7BmWAxGJnM4Teyawb
                                                uUtg8cPwrvK7zeRCRAcQDNVMqti+pP2l3q4VfN/FCOPKHdtXgeJ28FeHM7XxjoE1AcgLGboy8gZss5jdX4JtjXT1faGrYpCvSSwzhkvcd55qihQL6w2rMDl5i12/rimJy26wAwD
                                                wrMz2UXYDAGON+T2w7YIn6JbCQml8vnPP7HWTOp02ye0hsL/jFQKAp7Lsc+U2ALBCCvFbBG8It9HxYQ33P4twOaTKy7tUu3fimmtbqoldS+WuAwA6c3ne/wQYAP4bCK37qqp9AAAAAElFTkSuQmCC";

        public static string ValidFileXlsxBase64 = @"UEsDBBQACAgIAAoWO1IAAAAAAAAAAAAAAAALAAAAX3JlbHMvLnJlbHOtksFOwzAMhu97iir3Nd1ACKGmu0xIuyE0HsAkbhu1iaPEg/L2RBMSDI2yw45xfn/+YqXeTG4s3jAmS16JVVmJAr0mY32nxM
                                                v+cXkvNs2ifsYROEdSb0Mqco9PSvTM4UHKpHt0kEoK6PNNS9EB52PsZAA9QIdyXVV3Mv5kiOaEWeyMEnFnVqLYfwS8hE1tazVuSR8cej4z4lcikyF2yEpMo3ynOLwSDWWGCnneZX25y9/vlA4ZDDBI
                                                TRGXIebuyBbTt44h/ZTL6ZiYE7q55nJwYvQGzbwShDBndHtNI31ITO6fFR0zX0qLWp78y+YTUEsHCIWaNJruAAAAzgIAAFBLAwQUAAgICAAKFjtSAAAAAAAAAAAAAAAADwAAAHhsL3dvcmtib29rLnh
                                                tbI1T23LaMBB971d49A6+cCkwmAw1eJKZ3iakybNsr7GKLHmkJUA6/feuZZym0z70AaS96OzZ3ePlzbmW3jMYK7SKWTgMmAcq14VQ+5h9e0gHM+ZZ5KrgUiuI2QUsu1m9W560OWRaHzx6r2zMKsRm4f
                                                s2r6DmdqgbUBQptak5kmn2vm0M8MJWAFhLPwqCqV9zoViHsDD/g6HLUuSw0fmxBoUdiAHJkdjbSjSWrZalkPDYNeTxpvnMa6KdcJkzf/VK+6vxMp4fjk1K2TErubRAjVb69CX7DjlSR1xK5hUcIZwH4
                                                z7lDwiNlEllyNk6HgWc7O94azrEW23Ei1bI5S43WsqYoTleqxFRFPm/Irt2UA88s73z/CRUoU8xoxVd3txP7vokCqxogdPRbNz7bkHsK4zZLJxHzEOe3beDitkkoGelMBZdEYfCqZNnoHqtRQ35bzpy
                                                O+tPT7mBupdhS5XOu4IqO50ghZ6FFZkkxmYhKGDuisgh9jDUbk7zFwiG8hN9VEQhbDkZKD/pgiDWhHaNvy7nam9AIieSwyAIwhYXzvjRojuvUpKa7n/JSYrMQCcgpyXmHY2I2Y/302iazKbRIFqHo0E
                                                YbieDD6PxZJBu05Qml2ySefqTdOVQF/RLOv4WDX0k91DuLrTbc8y25xzk2nHyKa37d9T8XhOrX1BLBwg0mo9c+wEAAHADAABQSwMEFAAICAgAChY7UgAAAAAAAAAAAAAAAA0AAAB4bC9zdHlsZXMueG
                                                1s7Vhdb5swFH3fr7Dcl3ZaC8ny0W5A1nXKtJepWlNp0jpVDhiwamxknDbpr5+NgUDSbGumpumUvICv7zn35BgbbGcwTSi4xSIjnLmwdWRDgJnPA8IiF16OhofHEGQSsQBRzrALZziDA++Vk8kZxRcxx
                                                hIoBpa5MJYyfWdZmR/jBGVHPMVM9YRcJEiqpoisLBUYBZkGJdRq23bPShBh0HPYJBkmMgM+nzDpwk4VAubyJVDaeh0IDN0ZD5SUz5hhgSi0HkzuNpOv91+DvTd7e/aRbV8fvNfNq/0ycFUEDgeDvO/D
                                                9cEK1t4K1ibl3/P1m3ylIJ1tFaZ4TsjZ3JsuNAHPye7BLaKKpaXTfU65ACIau3A4tPNfXhMl2KSdIUrGguhgiBJCZybczsExEpkaSsOXVzc1FiotUJ4KYvyvE9pbBh+bDikmWPdtwrX8ogeOUFoNXBu
                                                agOekSEos2FA1QHE/mqVq+JmaYoYmz/tDdiTQrNXu1gD5RdUdcxGoKV1WbsEyBAKCIs4QvUxdGCKaYViFPvE7VgY9h+JQKmJBolhfJU8tTSIlT9RNidGlDXN1o8r7mNILvT58D+f/3lak03B5PrO8oZ
                                                Ydrb24NUxFA6UpnQ25JsnH0AQ+5imN0CklEUvwQuK54BL7Ml/e8rDnoDIRxFyQe0WtBzAqlhO9Gkri65D5vxBIPJXfuESGRWm6EygdqWBlImFBXlj1ZbEg7GbEh6TqVjallQxAuX+Dg1JkTAIFrWVa0
                                                3DBKXvuU2tdnwqdi0bVw3Wnysfg5Yhp78SsELP23NqJ2YnZidmJ2YlZR0yxBXkaOSs+cEo1z/GFs+xAp7VN3wqdzlapaW+TmpNnFmPVtwxmA1HbO3TX3TpMw2XldT3/KP2l7SMatnUeZ9sTL02+CmCx
                                                lZ71HuFZq7u5/eoWW7aJx+z/m5z9R6xpm3zQ9FHQdhhmFS+G2hFT9ZLowVoU6AM8F37VR6u09qSNJ4RKwkzLWgac8SRBdY9rgLcrAeCH/bMC9Rqg3oOgiRCY+bMK029gOr/DNGodN3D9h3DnWOiFooK
                                                cNCDm8HBupmrMD/a9X1BLBwgeHfe4JQMAAB0YAABQSwMEFAAICAgAChY7UgAAAAAAAAAAAAAAABgAAAB4bC93b3Jrc2hlZXRzL3NoZWV0MS54bWy9WE1z2zYQvfdXcHCv+KFvj6SMI1dNZ5woUznNTG
                                                8QCYoYgwALQFLsX58FQFEMYbU6NPJBBhbA2/d2KRPPs3ffShYciFRU8DmKexEKCE9FRvlujr48rX6doEBpzDPMBCdz9EIUerf4ZXYU8lkVhOgAALiao0Lr6i4MVVqQEqueqAiHlVzIEmuYyl2oKklwZ
                                                g+VLEyiaBSWmHLkEO7kNRgiz2lKHkS6LwnXDkQShjXQVwWtFFrMbIbPMsgp00R+FBnQzjFTBNYqvCMbor9Udl0/ic8QOC2Hi1lYH17MMgoZTFUCSfI5uo/vlnFsttgdf1FyVK1xoApxXAHRPcPqhGeD
                                                v0uaPVJOIKrlvg7+KY5LwT5AOaDy7YW/iRRNQNJdARQfSa4bSI23G8JIqknWPrfeawZJNi/lVrAGICM53jNtKEA6IU/xAzCeI27qygBSVCbFkjBmdKIgNXv/APzRAAWvQpSbFDOoUhxFrfkne7wbNfV
                                                8xC9ib8tSr5qHZSvEswkZ3Mh0yaow9a2webBqFijAED0Qx2YVj9sBdzZQ/9iWmMWmZQa6PT41Z2UfHuh2XQuow1ea6WKOJr3hKBk2RYKWfCCm4MAZoq/QiNO8Lr1wNX4kB8Jgt6XSjgG0kxb+kHkxg3
                                                oq+2kqy3ClTO9q0HSvtChrSq47Bc0ywt9Ma3OW+NscJfCbmm+s+Xq+mO6YOjuYeNSLB6Y0/2/Kfp2y/0bKJOlFU9sNJ9ZW4AFrvJhJcQykI2rPuvSuOk1+U/V+b+IxcZv/pRGWhKcRpJuc9y4pfMJh8
                                                6fhsIhm4cGwrHe8dzuS1o74xx1LHyNpdoQgrlGYdLXVdH+WuMQSi1rE+h1xbsfA7uB2x6g36MjzUQZvy/Na95Pl9T1iw468/hXyfJQL8gY3ljfwiI068gZXyPNRLsgb3lje0CM27sgbXiHPR7kgb3Rj
                                                eSOP2KQjb3SFPB/lgrzxjeWNPWLTjrzxFfJ8lAvyJjeWN/GIxd33wuQKfT7MBX3TG+ub+vo6b7X30yv0+TAX9MXRjQWahF2FSffNHl0h8Q2gSxrjW2uMfY3dF3y95z80+kBdjWHrvlZJyvW6so4qKMC
                                                igBU8W5rd2c50I+CrmvuckPRVcI3ZEmwaka3bJ3hNTVN/IXTm7COWOwqJmTU9UW9c26B6DC7BjuDFsBUaynuaFdZLmdkwjifRsPlBQS6EfnspbAzhvgIbUhG5oa/E3VVblienUmljbD7ty61FQs481n
                                                fouJ42/gEFBnYtLaNMHPlTQfgalEP/JQXh1q/OUSWklpiC6dkynD7f8+xrQXXjR4NM4pb1S8EBLUVpjK8y7o1DbK/Iqsvu9Fw2PXioKFzfjJJT8c+RVFSU2PstFMMVcWXrFWQ0z6FBXNsEZ06n8DrLf
                                                jucH/fFTGSZc7TwPLXGMHSILtyM28lg2vxDYfEdUEsHCMEwlWY2BAAAlBAAAFBLAwQUAAgICAAKFjtSAAAAAAAAAAAAAAAAFAAAAHhsL3NoYXJlZFN0cmluZ3MueG1sldLBTsMwDAbgO08R+c6SdGOM
                                                Kc00JvoAaHuAqDVrUeOUOp3g7UnhwA01pzj2F/+XmMOn78UNR+4ClaBXCgRSHZqOriVcztX9DgRHR43rA2EJX8hwsHeGOYr0lLiENsZhLyXXLXrHqzAgpclbGL2L6TpeJQ8juoZbxOh7WSi1ld51BKI
                                                OE8USijWIibqPCU+/Db0Ba7iz5idkz4OrU3bawjjeEOwlneQ8GhmtkTP8Bx/9vHQRfUXHgRZRXTy/nI9VpZRe5E/Be6QoXPM+cZzLzJgi068z/SbTP2T6baZ/zPS7TP+U57X68zL9fvsNUEsHCGJi80
                                                DtAAAAOwMAAFBLAwQUAAgICAAKFjtSAAAAAAAAAAAAAAAAGgAAAHhsL19yZWxzL3dvcmtib29rLnhtbC5yZWxzrZFNa8MwDIbv/RVG98VJB2OMOL2MQa/9+AHGUeLQxDaS1rX/fi4bWwpl7NCT0Nfzv
                                                kj16jSN6ojEQwwGqqIEhcHFdgi9gf3u7eEZVs2i3uBoJY+wHxKrvBPYgBdJL1qz8zhZLmLCkDtdpMlKTqnXybqD7VEvy/JJ05wBzRVTrVsDtG4rULtzwv+wY9cNDl+je58wyA0JzXIekTPRUo9i4Csv
                                                Mgf0bfnlPeU/Ih3YI8qvg59SNncJ1V9mHu96C28J261Qfuz8JPPyt5lFra/e3XwCUEsHCE/w+XrSAAAAJQIAAFBLAwQUAAgICAAKFjtSAAAAAAAAAAAAAAAAEQAAAGRvY1Byb3BzL2NvcmUueG1sfVJ
                                                NU4MwEL37K5jcIUA/bDOFzqjTi9ZxRjo63mLY0iiETJKW8u8NULBqx9vuvpe3+za7WB6L3DmA0rwUEQo8HzkgWJlykUVok6zcGXK0oSKleSkgQjVotIyvFkwSVip4UqUEZThoxwoJTZiM0M4YSTDWbA
                                                cF1Z5lCAtuS1VQY1OVYUnZJ80Ah74/xQUYmlJDcSPoykERnSRTNkjKvcpbgZRhyKEAYTQOvAB/cw2oQl980CJnzIKbWsJFag8O7KPmA7GqKq8atVQ7f4Bf1w/PrVWXi2ZVDFC8OA1CmAJqIHWsAOna9
                                                cjL6PYuWaE49EPfDUI3nCf+lExmZBK8LfCv941gF5cqvocDF85jtq9BNMwBaEgpaKa4NPY/4xb8UbB5TkW2t8uPQbib55YylJpvzak2a3sAWw7pTW01LtT66YpT7X97gesHbnid+HMynpHw3F4v0HZW
                                                1ldzh/G4bTqkzdR6//4BzHSWhsTGhpscunIf/rnN+AtQSwcIeQvUkHYBAADnAgAAUEsDBBQACAgIAAoWO1IAAAAAAAAAAAAAAAAQAAAAZG9jUHJvcHMvYXBwLnhtbJ2Qy07DMBBF93xFZLFtnIQ8K8c
                                                VEmKFBItA2UWOPW6N4odiU9q/x4DUds1u7tzRuTNDNkc9JwdYvLKmR3maoQQMt0KZXY9eh8dVixIfmBFstgZ6dAKPNvSGvCzWwRIU+CQSjO/RPgS3xtjzPWjm02ib6Ei7aBaiXHbYSqk4PFj+qcEEXG
                                                RZjeEYwAgQK3cGoj/i+hD+CxWW/+zn34aTizxKBtBuZgEowZdysIHNg9JA72L7LMi9c7PiLMSP0Cc1LfD8G4HrtEyLtLjdKiPslx/f23qsy+RqZIxHfAAPuISyyUXLs4J3vMtklReykR0w0bZNNVWN5
                                                FMOPOZehxF8+Sr9BlBLBwggcbPqAgEAAJoBAABQSwMEFAAICAgAChY7UgAAAAAAAAAAAAAAABMAAABkb2NQcm9wcy9jdXN0b20ueG1stdNNb4IwGAfw+z4F6R1bUFQMYBQ0O+ywxJd7V4o0g7ZpqxtZ
                                                9t1XhujRZNPb05f8f+2TNpp/1pVzokozwWPgDRBwKCciZ/wQg9127U6Bow3mOa4EpzFoqAbz5Cl6VUJSZRjVjk3gOgalMXIGoSYlrbEe2GVuVwqhamzsUB2gKApGaCbIsabcQB+hMSRHbUTtyksc6PJ
                                                mJ/PXyFyQ9nR6v22kzUuic3jjFLVheQy+siDNsgAFrr8KU9dD3tINh+HERVOE/KWfrsPF6hs4st3sA4fj2t58IeW+65ONPJlZJT+0UYkXDNAQoQhepyLYi/+0h71tL7ih5KiYaTqcjZKOtMXduFHPPd
                                                vGqYrxd52WmB9o3qFvQlRn9re8Gxz08Etr7uRWZNjQB6PjHt0QXNHURj0YnFzAEqv2zd724PWbJT9QSwcI+M5UZkABAACrAwAAUEsDBBQACAgIAAoWO1IAAAAAAAAAAAAAAAATAAAAW0NvbnRlbnRfV
                                                HlwZXNdLnhtbL2Uy07DMBBF9/2KyFsUu2WBEErbBY8lVKKskbEniWn8kO2W9u+ZJFBVJbRUjVhZ8cy9Z2ZiO5uudZWswAdlzZiM6JAkYISVyhRj8jJ/SK/JdDLI5hsHIcFcE8akjNHdMBZECZoHah0Y
                                                jOTWax7x0xfMcbHgBbDL4fCKCWsimJjG2oNMsjvI+bKKyf0at1suykly2+bVqDHhzlVK8IhhVkdZp85DFQ4IV0buVZd+VUZR2eSEUrlw8TvBmWIPoHTdWb3frXh30C1pAqh5wnF7JSGZcR8fucYE9lp
                                                3wmjP/XSR1hX7sH7xZu2CHh57B83muRIgrVhqlNDgPHAZSoCoK9qsVHNljvBD3FQQ+qY3pn/ovBEE1iyjnovY+h+bQMk9yOfo8Zr1Pohd7yN1tMdu9zz8xxHEwmfeuoAvg4fTu//m1erUoRH4qA7/+i
                                                0Rrc8eN9R3XYI8lS2WIVp9Nr61+QkfZKx5pSefUEsHCM/eIr5nAQAA1AUAAFBLAQIUABQACAgIAAoWO1KFmjSa7gAAAM4CAAALAAAAAAAAAAAAAAAAAAAAAABfcmVscy8ucmVsc1BLAQIUABQACAgIA
                                                AoWO1I0mo9c+wEAAHADAAAPAAAAAAAAAAAAAAAAACcBAAB4bC93b3JrYm9vay54bWxQSwECFAAUAAgICAAKFjtSHh33uCUDAAAdGAAADQAAAAAAAAAAAAAAAABfAwAAeGwvc3R5bGVzLnhtbFBLAQIU
                                                ABQACAgIAAoWO1LBMJVmNgQAAJQQAAAYAAAAAAAAAAAAAAAAAL8GAAB4bC93b3Jrc2hlZXRzL3NoZWV0MS54bWxQSwECFAAUAAgICAAKFjtSYmLzQO0AAAA7AwAAFAAAAAAAAAAAAAAAAAA7CwAAeGw
                                                vc2hhcmVkU3RyaW5ncy54bWxQSwECFAAUAAgICAAKFjtST/D5etIAAAAlAgAAGgAAAAAAAAAAAAAAAABqDAAAeGwvX3JlbHMvd29ya2Jvb2sueG1sLnJlbHNQSwECFAAUAAgICAAKFjtSeQvUkHYBAA
                                                DnAgAAEQAAAAAAAAAAAAAAAACEDQAAZG9jUHJvcHMvY29yZS54bWxQSwECFAAUAAgICAAKFjtSIHGz6gIBAACaAQAAEAAAAAAAAAAAAAAAAAA5DwAAZG9jUHJvcHMvYXBwLnhtbFBLAQIUABQACAgIA
AoWO1                                           L4zlRmQAEAAKsDAAATAAAAAAAAAAAAAAAAAHkQAABkb2NQcm9wcy9jdXN0b20ueG1sUEsBAhQAFAAICAgAChY7Us/eIr5nAQAA1AUAABMAAAAAAAAAAAAAAAAA+hEAAFtDb250ZW50X1R5cGVz
                                                XS54bWxQSwUGAAAAAAoACgCAAgAAohMAAAAA";

        public static string InvalidFileXlsxBase64 = @"UEsDBBQACAgIAOc5hVIAAAAAAAAAAAAAAAALAAAAX3JlbHMvLnJlbHOtksFOwzAMhu97iir3Nd1ACKGmu0xIuyE0HsAkbhu1iaPEg/L2RBMSDI2yw45xfn/+YqXeTG4s3jAmS16JVVmJAr0m
                                                Y32nxMv+cXkvNs2ifsYROEdSb0Mqco9PSvTM4UHKpHt0kEoK6PNNS9EB52PsZAA9QIdyXVV3Mv5kiOaEWeyMEnFnVqLYfwS8hE1tazVuSR8cej4z4lcikyF2yEpMo3ynOLwSDWWGCnneZX25y9/vlA4
                                                ZDDBITRGXIebuyBbTt44h/ZTL6ZiYE7q55nJwYvQGzbwShDBndHtNI31ITO6fFR0zX0qLWp78y+YTUEsHCIWaNJruAAAAzgIAAFBLAwQUAAgICADnOYVSAAAAAAAAAAAAAAAADwAAAHhsL3dvcmtib2
                                                9rLnhtbI1T23LaMBB971d49A6+cCkwmAw1eJKZ3iakybNsr7GKLHmkJUA6/feuZZym0z70AaS96OzZ3ePlzbmW3jMYK7SKWTgMmAcq14VQ+5h9e0gHM+ZZ5KrgUiuI2QUsu1m9W560OWRaHzx6r2zMK
                                                sRm4fs2r6DmdqgbUBQptak5kmn2vm0M8MJWAFhLPwqCqV9zoViHsDD/g6HLUuSw0fmxBoUdiAHJkdjbSjSWrZalkPDYNeTxpvnMa6KdcJkzf/VK+6vxMp4fjk1K2TErubRAjVb69CX7DjlSR1xK5hUc
                                                IZwH4z7lDwiNlEllyNk6HgWc7O94azrEW23Ei1bI5S43WsqYoTleqxFRFPm/Irt2UA88s73z/CRUoU8xoxVd3txP7vokCqxogdPRbNz7bkHsK4zZLJxHzEOe3beDitkkoGelMBZdEYfCqZNnoHqtRQ3
                                                5bzpyO+tPT7mBupdhS5XOu4IqO50ghZ6FFZkkxmYhKGDuisgh9jDUbk7zFwiG8hN9VEQhbDkZKD/pgiDWhHaNvy7nam9AIieSwyAIwhYXzvjRojuvUpKa7n/JSYrMQCcgpyXmHY2I2Y/302iazKbRIF
                                                qHo0EYbieDD6PxZJBu05Qml2ySefqTdOVQF/RLOv4WDX0k91DuLrTbc8y25xzk2nHyKa37d9T8XhOrX1BLBwg0mo9c+wEAAHADAABQSwMEFAAICAgA5zmFUgAAAAAAAAAAAAAAAA0AAAB4bC9zdHlsZ
                                                XMueG1s7VldT9swFH3fr7DMC0yDpF0/YEvSMaZOe5nQKNKkMSE3cRILx44cF1p+/ew4SZOWbqMTpUzlJfH1PeeeHsdObJzBNKHgFouMcObC1pENAWY+DwiLXHg5Gh4eQ5BJxAJEOcMunOEMDrxXTiZn
                                                FF/EGEugGFjmwljK9J1lZX6ME5Qd8RQz1RNykSCpmiKyslRgFGQalFCrbds9K0GEQc9hk2SYyAz4fMKkCztVCJjLl0Bp63UgMHRnPFBSPmOGBaLQejC520y+3n8N9t7s7dlHtn198F43r/bLwFUROBw
                                                M8r4P1wcrWHsrWJuUf8/Xb/KVgnS2VZjiOSFnc2+60AQ8J7sHt4gqlpZO9znlAoho7MLh0M7/8poowSbtDFEyFkQHQ5QQOjPhdg6OkcjUUBq+vLqpsVBpgfJUEON/ndDeMvjYdEgxwbpvE67lFz1whN
                                                Jq4NrQBDwnRVJiwYaqAYr70SxVw8/UFDM0ed4fsiOBZq12twbIL6rumItATemycguWIRAQFHGG6GXqwhDRDMMq9InfsTLoORSHUhELEsX6KnlqaRIpeaJuSowubZirG1Xex5Re6PXhezj/9bYinYbL8
                                                5nlDbXsaO3FrWEqGihN6WzINUk+hibwMU9phE4piViCFxLPBZfYl/nyloc9B5WJIOaC3CtqPYBRsZzo1VASX4fM74VA4qn8xiUyLErTnUDpSAUrEwkL8sKqL4sFYTcjPiRVt7IprWQAyv0bHJQiYxIo
                                                aC3TmoYLTtlzn1rr+lToXDSqHq47VT4GL0dMeydmhZi159ZOzE7MTsxOzE7MOmKKLcjTyFnxgVOqeY4vnGUHOq1t+lbodLZKTXub1Jw8sxirvmUwG4ja3qG37tZhGi4rr+v5R+kvbR/RsK3zONueeGn
                                                yVQCLrfSs9wjPWt3N7Ve32LJNPGb/3+TsP2JN2+SDpo+CttKw9V8Cm//QKmfr5k2zirdp7Vyu8WatokCferrwqz6PpjXfxhNCJWGmZS0DzniSoPqDWQO8XQkAP+yfFajXAPUeBE2EwMyfVZh+A9P5Ha
                                                ZR67iB6z+EO8dCj1cFOWlAzInr3EzVmP83xPsFUEsHCByFQP8wAwAAUhkAAFBLAwQUAAgICADnOYVSAAAAAAAAAAAAAAAAGAAAAHhsL3dvcmtzaGVldHMvc2hlZXQxLnhtbL1XTZPaOBC951e4dA/+Z
                                                sgUkJowYZOqSUgtk6Rqb8KWsWpkySvJkJlfvy3JGAdYikMYDiC1Wt39Xsv4afz+V8W8DZGKCj5B4SBAHuGZyClfT9D3x/nbEfKUxjzHTHAyQc9EoffTN+OtkE+qJER7EICrCSq1rm99X2UlqbAaiJpw
                                                WCmErLCGqVz7qpYE53ZTxfwoCIZ+hSlHLsKtvCSGKAqakXuRNRXh2gWRhGEN5auS1gpNxzbDN+kVlGkiv4gcyi4wUwTWarwmS6K/13ZdP4pvYNgt+9Ox326ejnMKGQwrniTFBN2Ft7MwSEbpzdD4Wbc
                                                flGxVb+ypUmznUG3DsNoFtca/JM0fKCdg1bJpjX+L7UywT8AJ0N9f+IdI0RkkXZdQ5wMpdBdS49WSMJJpkvf3LRrNIMnyuVoJ1gXISYEbpk0JkE7InX0DFU8QN+QyCClqk2JGGDNgkZcZ388Qf5gg70
                                                WIaplhBlSFQdCbf7XbD62G1Af8LBpLS7tqTsxKiCdjMnED0yqLwpBcY3O62iqQh8G6Ia6aedyfu62e+te2Bda6rpnA/fGuNXN7fqDhLRPAwk+a63KCRoP0JojDKO1Ygp58IoZxKBqsL9CJ3bzlXjiSH
                                                8iGMPC2xfRtEN1h839LPh0Docp+G2oZrpVpXhs0a5QWVVuVa09J85zwk2ltzgr/mqAIfql5bs1D+mzaY4h2YcLhIEwMO382ZdymjE+kjKJB8M42xIG1DNxjjadjKbaedIXavS69Y6fLb1iPB6OjSpzz
                                                mUbYIo4wAnST884lhW/YbP4gNtNg7G9Mla3HB+cR9TzC3z1mxzGizsMHcB3C6BBbW+61wEW2sKBXWHwAznkk1oNbj+EgOYB3HCU5De+odVeGF9vC0l5h6QG8+AJ48aXwkleGlxzBGx7ASy6Al1wKL31
                                                leOlRYTcH8NIL4B1H+R947gUdngMZ/UmQJ5Kfffyvnfzsw3nt5GcfnWsnP3uwr518+MrJ/d57tSJybXWRAv+Gm4yoZ23Fa3x7Z6WAv3cHLSwp14vaSmevBBkKmn8vW9d7yXpoAQHdvbKFpC+Ca8xmoM
                                                eJ7AkMuFRomh0v+E6Ff8FyTSExs8I2GNy0UrcdgxK0I2jsSmhgazcrrV42szQMR0HafZBXCKFPL/md8m9qkJo1kUv6Qpwc6cnagkqljXj92lQrGwm5W0Irk8J22klE5JmwC2krysWWP5aELwA5tFNSA
                                                G4vJhNUC6klpiBsVwxnT3c8/1lS3V08vFzinrzPoD8zUZkbjjIKnYOtUWR+WN3umHU9uK8pvKENkh35e0smakqshAEyHIlzy5eX06KABnFtE+xr2pkXef5xsz+907HIc3drgQPVG8PQRXTmbtxPBtPu
                                                5jj9D1BLBwj7pq0XDgQAAH0OAABQSwMEFAAICAgA5zmFUgAAAAAAAAAAAAAAABQAAAB4bC9zaGFyZWRTdHJpbmdzLnhtbJWRzU7DMAyA7zxF5DtLWxCapjTTNtEHQNsDWK1Zgxqni9MJ3p4UDtxQe/L
                                                fZ38Hm/2nH9SdorjANZSbAhRxGzrH1xou5+ZxC0oScodDYKrhiwT29sGIJJVXWWroUxp3Wkvbk0fZhJE4T95D9JhyGa9axkjYSU+U/KCronjRHh2DasPEKWtLUBO720Sn38YWrBFnzY9jJyO2WZ2PCM
                                                U7gb3kyOjJ6GSNnsF/4IOfby5C3wgl8CK0rI6v50PTFEW5iD8F74mTwu5jkjSnKzXVSv5pJf/8x+v8XPsNUEsHCL8/MBjaAAAAGgIAAFBLAwQUAAgICADnOYVSAAAAAAAAAAAAAAAAGgAAAHhsL19yZ
                                                WxzL3dvcmtib29rLnhtbC5yZWxzrZFNa8MwDIbv/RVG98VJB2OMOL2MQa/9+AHGUeLQxDaS1rX/fi4bWwpl7NCT0Nfzvkj16jSN6ojEQwwGqqIEhcHFdgi9gf3u7eEZVs2i3uBoJY+wHxKrvBPYgBdJ
                                                L1qz8zhZLmLCkDtdpMlKTqnXybqD7VEvy/JJ05wBzRVTrVsDtG4rULtzwv+wY9cNDl+je58wyA0JzXIekTPRUo9i4CsvMgf0bfnlPeU/Ih3YI8qvg59SNncJ1V9mHu96C28J261Qfuz8JPPyt5lFra/
                                                e3XwCUEsHCE/w+XrSAAAAJQIAAFBLAwQUAAgICADnOYVSAAAAAAAAAAAAAAAAEQAAAGRvY1Byb3BzL2NvcmUueG1sfVJNT8MwDL3zK6rcuzTdOka0dhKgXWAIaZ1A3ELqdYE2jZJ0H/+etFvLgImb7f
                                                fy7Od4OtuXhbcFbUQlY0QGAfJA8ioTMo/RKp37E+QZy2TGikpCjA5g0Cy5mnJFeaXhWVcKtBVgPCckDeUqRhtrFcXY8A2UzAwcQzpwXemSWZfqHCvGP1kOOAyCMS7BsoxZhhtBX/WK6CSZ8V5S1bpoB
                                                TKOoYASpDWYDAj+5lrQpbn4oEXOmKWwBwUXqR3Ys/dG9MTdbjfYDVuqm5/g18XjsrXqC9msigNKpqdBKNfALGSeE6DHdh3yMry7T+coCYMw8EnohzdpMKbRhEbkbYp/vW8Ej3GlkwfYCuk95fUBZMPs
                                                gYaUgeFaKOv+M2nBHwWXF0zmtVt+AtJfLVtKX2q+tWDGLtwBrAVktwencaHWTVeeav/bI34w8oMoJSNKIkpGZ/Y6gbazdr6aO0yu26Z92kxt6vcP4PZoqU9cbIUt4Fjuwj+3mXwBUEsHCHnhueR3AQA
                                                A5wIAAFBLAwQUAAgICADnOYVSAAAAAAAAAAAAAAAAEAAAAGRvY1Byb3BzL2FwcC54bWydkMtOwzAQRfd8RWSxbZyEPCvHFRJihQSLQNlFjj1ujeKHYlPav8eA1HbNbu7c0bkzQzZHPScHWLyypkd5mq
                                                EEDLdCmV2PXofHVYsSH5gRbLYGenQCjzb0hrws1sESFPgkEozv0T4Et8bY8z1o5tNom+hIu2gWolx22EqpODxY/qnBBFxkWY3hGMAIECt3BqI/4voQ/gsVlv/s59+Gk4s8SgbQbmYBKMGXcrCBzYPSQ
                                                PO72D8rcu/crDgL8SX0SU0LPP9m4Dot0yItbrfKCPvlx/e2HusyuRoZ4xUfwAMuoWxy0fKs4B3vMlnlhWxkB0y0bVNNVSP5lAOPuddhBF/eSr8BUEsHCC6odSUDAQAAmwEAAFBLAwQUAAgICADnOYVS
                                                AAAAAAAAAAAAAAAAEwAAAGRvY1Byb3BzL2N1c3RvbS54bWy1001vgjAYB/D7PgXpHVtQVAxgFDQ77LDEl3tXijSDtmmrG1n23VeG6NFk09vTl/x/7ZM2mn/WlXOiSjPBY+ANEHAoJyJn/BCD3XbtToG
                                                jDeY5rgSnMWioBvPkKXpVQlJlGNWOTeA6BqUxcgahJiWtsR7YZW5XCqFqbOxQHaAoCkZoJsixptxAH6ExJEdtRO3KSxzo8mYn89fIXJD2dHq/baTNS6JzeOMUtWF5DL6yIM2yAAWuvwpT10Pe0g2H4c
                                                RFU4T8pZ+uw8XqGziy3ewDh+Pa3nwh5b7rk408mVklP7RRiRcM0BChCF6nItiL/7SHvW0vuKHkqJhpOpyNko60xd24Uc8928apivF3nZaYH2jeoW9CVGf2t7wbHPTwS2vu5FZk2NAHo+Me3RBc0dRGP
                                                RicXMASq/bN3vbg9ZslP1BLBwj4zlRmQAEAAKsDAABQSwMEFAAICAgA5zmFUgAAAAAAAAAAAAAAABMAAABbQ29udGVudF9UeXBlc10ueG1svZTLTsMwEEX3/YrIWxS7ZYEQStsFjyVUoqyRsSeJafyQ
                                                7Zb275kkUFUltFSNWFnxzL1nZmI7m651lazAB2XNmIzokCRghJXKFGPyMn9Ir8l0MsjmGwchwVwTxqSM0d0wFkQJmgdqHRiM5NZrHvHTF8xxseAFsMvh8IoJayKYmMbag0yyO8j5sorJ/Rq3Wy7KSXL
                                                b5tWoMeHOVUrwiGFWR1mnzkMVDghXRu5Vl35VRlHZ5IRSuXDxO8GZYg+gdN1Zvd+teHfQLWkCqHnCcXslIZlxHx+5xgT2WnfCaM/9dJHWFfuwfvFm7YIeHnsHzea5EiCtWGqU0OA8cBlKgKgr2qxUc2
                                                WO8EPcVBD6pjemf+i8EQTWLKOei9j6H5tAyT3I5+jxmvU+iF3vI3W0x273PPzHEcTCZ966gC+Dh9O7/+bV6tShEfioDv/6LRGtzx431HddgjyVLZYhWn02vrX5CR9krHmlJ59QSwcIz94ivmcBAADUB
                                                QAAUEsBAhQAFAAICAgA5zmFUoWaNJruAAAAzgIAAAsAAAAAAAAAAAAAAAAAAAAAAF9yZWxzLy5yZWxzUEsBAhQAFAAICAgA5zmFUjSaj1z7AQAAcAMAAA8AAAAAAAAAAAAAAAAAJwEAAHhsL3dvcmti
                                                b29rLnhtbFBLAQIUABQACAgIAOc5hVIchUD/MAMAAFIZAAANAAAAAAAAAAAAAAAAAF8DAAB4bC9zdHlsZXMueG1sUEsBAhQAFAAICAgA5zmFUvumrRcOBAAAfQ4AABgAAAAAAAAAAAAAAAAAygYAAHh
                                                sL3dvcmtzaGVldHMvc2hlZXQxLnhtbFBLAQIUABQACAgIAOc5hVK/PzAY2gAAABoCAAAUAAAAAAAAAAAAAAAAAB4LAAB4bC9zaGFyZWRTdHJpbmdzLnhtbFBLAQIUABQACAgIAOc5hVJP8Pl60gAAAC
                                                UCAAAaAAAAAAAAAAAAAAAAADoMAAB4bC9fcmVscy93b3JrYm9vay54bWwucmVsc1BLAQIUABQACAgIAOc5hVJ54bnkdwEAAOcCAAARAAAAAAAAAAAAAAAAAFQNAABkb2NQcm9wcy9jb3JlLnhtbFBLA
                                                QIUABQACAgIAOc5hVIuqHUlAwEAAJsBAAAQAAAAAAAAAAAAAAAAAAoPAABkb2NQcm9wcy9hcHAueG1sUEsBAhQAFAAICAgA5zmFUvjOVGZAAQAAqwMAABMAAAAAAAAAAAAAAAAASxAAAGRvY1Byb3Bz
                                                L2N1c3RvbS54bWxQSwECFAAUAAgICADnOYVSz94ivmcBAADUBQAAEwAAAAAAAAAAAAAAAADMEQAAW0NvbnRlbnRfVHlwZXNdLnhtbFBLBQYAAAAACgAKAIACAAB0EwAAAAA=";
    }
}
