TODO: Setup readme



https://ucsfxknrgbc75k3w4mlbabzet4.appsync-api.eu-north-1.amazonaws.com/graphql



x-api-key

da2-25z2e2lovnf4pnbdsomoti5bju



Content-Type

application/json



{

&nbsp;   "query": "mutation {  createPreference(houseNumber:\\"33\\" postalCode: \\"3053XJ\\" street: \\"Clematisstraat\\" city: \\"ROTTERDAM\\" country: \\"NL\\" notAtHome: { postNLPoint: \\"Point123\\" parcelLocker: \\"Locker456\\" } deliveryMethod: PostNLPunt){ id } }"

}



{

&nbsp;   "query": "query {  getPreferenceById(id: \\"3b1c8c0c-566c-47b2-b64b-cb96f656722a\\"){ id street houseNumber postalCode city country notAtHome{ postNLPoint parcelLocker } deliveryMethod } }"

}

