mergeInto(LibraryManager.library, {
OtherFunc: function(b) {
	FromJsToUnity(b);
},
PluginTestWeb: function(a) {
    FromUnityToJs (a);
},
TestMySql: function(x) {
	FromUnityToMySql(x);
}

});