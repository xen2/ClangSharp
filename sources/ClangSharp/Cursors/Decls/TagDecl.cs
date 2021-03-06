using System;
using System.Collections.Generic;
using System.Linq;
using ClangSharp.Interop;

namespace ClangSharp
{
    public unsafe class TagDecl : TypeDecl, IDeclContext, IRedeclarable<TagDecl>
    {
        private readonly Lazy<IReadOnlyList<Decl>> _decls;
        private readonly Lazy<TagDecl> _definition;

        private protected TagDecl(CXCursor handle, CXCursorKind expectedKind) : base(handle, expectedKind)
        {
            _decls = new Lazy<IReadOnlyList<Decl>>(() => CursorChildren.Where((cursor) => cursor is Decl).Cast<Decl>().ToList());
            _definition = new Lazy<TagDecl>(() => TranslationUnit.GetOrCreate<TagDecl>(Handle.Definition));
        }

        public IReadOnlyList<Decl> Decls => _decls.Value;

        public TagDecl Definition => _definition.Value;

        public IDeclContext LexicalParent => LexicalDeclContext;

        public IDeclContext Parent => DeclContext;
    }
}
